using Microsoft.EntityFrameworkCore;
using Surveys.Commons;
using Surveys.Commons.Dtos.QuestionDtos;
using Surveys.Commons.Dtos.SurveyDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
                Questions = SurveyQuestions.Select(x => new QuestionDto { Text = x.Question.Text }).ToList()
            };
            return result;
        }

        public async Task<List<Survey>> GetSurveys(IRepository repository)
        {
            return await repository.Surveys.ToListAsync();
        }
    }
}
