using Microsoft.EntityFrameworkCore;
using Surveys.Commons;
using Surveys.Commons.Dtos.QuestionDtos;
using Surveys.Commons.Dtos.SurveyDtos;
using Surveys.Commons.Dtos.SurveyQuestionAnswerDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Surveys.Domain.Entities
{
    public class Survey
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string CreatorEmail { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        public async Task<Result<SurveyDetailsDto>> GetDetails(Guid surveyId, IRepository repository)
        {
            var result = new Result<SurveyDetailsDto>();
            var survey = await repository.Surveys.Where(x => x.Id == surveyId).Include(x => x.SurveyQuestions)
                .ThenInclude(x => x.Question).FirstOrDefaultAsync();
            Id = survey.Id;
            Name = survey.Name;
            SurveyQuestions = survey.SurveyQuestions;
            result.Value = new SurveyDetailsDto
            {
                Id = Id,
                Name = Name,
                Questions = SurveyQuestions.Select(x =>
                    new QuestionDto
                    {
                        SurveyQuestionId = x.Id,
                        AnswerType = (int)x.Question.AnswerType,
                        Text = x.Question.Text
                    })
                    .ToList()
            };
            return result;
        }

        public async Task<List<Survey>> GetSurveys(IRepository repository)
        {
            return await repository.Surveys.ToListAsync();
        }
        public async Task<Result> SubmitAnswers(ClaimsPrincipal user, List<SurveyAnswerDto> answersDtos, IRepository repository)
        {
            var result = new Result();
            var userFromDb = await repository.Users.FirstOrDefaultAsync(x => x.Email == user.Identity.Name);
            var now = DateTime.Now;
            var answers = new List<SurveyQuestionAnswer>();
            foreach (var surveyAnswerDto in answersDtos)
            {
                var answer = new SurveyQuestionAnswer()
                {
                    Id = Guid.NewGuid(),
                    User = userFromDb,
                    Answer = surveyAnswerDto.Answer,
                    SubmittedAt = now,
                    SurveyQuestion =
                        await repository.SurveyQuestions.FirstOrDefaultAsync(x =>
                            x.Id == surveyAnswerDto.SurveyQuestionId)
                };
                answers.Add(answer);
            }

            await repository.SurveyQuestionAnswers.AddRangeAsync(answers);
            await repository.SaveChangesAsync();
            return result;
        }

        public async Task<List<SurveyListDto>> GetUsersSurveys(string? identityName, IRepository repository)
        {
            var result = new List<SurveyListDto>();
            var userFromDb = await repository.Users.FirstOrDefaultAsync(x => x.Email == identityName);
            var answers = await repository.SurveyQuestionAnswers
                .Include(x => x.SurveyQuestion)
                .ThenInclude(x => x.Survey)
                .ToListAsync();
            var surveys = answers.Where(x => x.User.Id == userFromDb.Id).Select(x => x.SurveyQuestion.Survey).Distinct().ToList();
            foreach (var survey in surveys)
            {
                result.Add(new SurveyListDto
                {
                    Id = survey.Id,
                    Name = survey.Name
                });
            }

            return result;
        }

        public async Task<Result<SurveyAnswersDto>> GetUsersSurveysAnswers(string? identityName, Guid surveyId, IRepository repository)
        {
            var survey = await repository.Surveys.FirstOrDefaultAsync(x => x.Id == surveyId);
            var answers = await repository.SurveyQuestionAnswers
                .Include(x => x.SurveyQuestion)
                .ThenInclude(x => x.Question)
                .Include(x => x.User)
                .Where(x => x.User.Email == identityName &&
                            x.SurveyQuestion.Survey.Id == survey.Id)
                .ToListAsync();

            var questionAnswerDtos = new List<QuestionAnswerDto>();
            foreach (var surveyQuestionAnswer in answers)
            {
                questionAnswerDtos.Add(new QuestionAnswerDto
                {
                    SurveyQuestionId = surveyQuestionAnswer.SurveyQuestion.Id,
                    Text = surveyQuestionAnswer.SurveyQuestion.Question.Text,
                    Answer = surveyQuestionAnswer.Answer
                }
                );
            }

            var result = new Result<SurveyAnswersDto>
            {
                Value = new SurveyAnswersDto
                {
                    Name = survey.Name,
                    Id = survey.Id,
                    QuestionAnswerDtos = questionAnswerDtos
                }
            };
            return result;
        }
    }
}
