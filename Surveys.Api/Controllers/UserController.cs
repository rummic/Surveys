using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Surveys.Api.Validators;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.DataAccess;
using Surveys.Domain;
using Surveys.Domain.Entities;

namespace Surveys.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IRepository _repository;

        public UserController(ILogger<UserController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userData)
        {
            var validatorResult = UserValidator.ValidateRegisterUser(userData);
            if (validatorResult.HasError)
            {
                return BadRequest(validatorResult);
            }

            User newUser = new User(userData);
            Result result = await newUser.Register(_repository);
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
