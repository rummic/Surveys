using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Surveys.Commons;
using Surveys.Commons.Dtos.SurveyDtos;
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
        [AllowAnonymous]
        [HttpGet]
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
        [AllowAnonymous]
        [HttpPost("GetDetails")]
        public async Task<IActionResult> GetQuestions([FromBody] GetSurveyDetailsDto surveyDetails)
        {
            var survey = new Survey();
            Result<SurveyDetailsDto> result;
            try
            {
                result = await survey.GetDetails(surveyDetails.Id, _repository);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok(result);
        }
    }
}
