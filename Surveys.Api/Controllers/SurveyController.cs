using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Surveys.Commons;
using Surveys.Commons.Dtos.SurveyDtos;
using Surveys.Commons.Dtos.SurveyQuestionAnswerDtos;
using Surveys.Domain;
using Surveys.Domain.Entities;

namespace Surveys.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly IRepository _repository;
        public SurveyController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetSurveys()
        {
            var survey = new Survey();
            var result = await survey.GetSurveys(_repository);
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestions(Guid id)
        {
            var survey = new Survey();
            Result<SurveyDetailsDto> result;
            try
            {
                result = await survey.GetDetails(id, _repository);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerSurvey([FromBody] List<SurveyAnswerDto> answersDtos)
        {
            var result = await new Survey().SubmitAnswers(User, answersDtos, _repository);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersSurveys()
        {
            var result = await new Survey().GetUsersSurveys(User.Identity.Name, _repository);
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}/answers")]
        public async Task<IActionResult> GetUsersSurveyAnswers(Guid id)
        {
            var result = await new Survey().GetUsersSurveysAnswers(User.Identity.Name, id, _repository);
            if (result.HasError)
            {
                return BadRequest();
            }
            return Ok(result);
        }

    }
}
