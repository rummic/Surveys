using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Surveys.Api.Validators;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Domain;
using Surveys.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Surveys.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly AuthorizationProvider _authorizationProvider;

        public UserController(IRepository repository, AuthorizationProvider authorizationProvider)
        {
            _repository = repository;
            _authorizationProvider = authorizationProvider;
        }

        [AllowAnonymous]
        [HttpPost("register")]
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginUserDto loginUserDto)
        {
            var result = await _authorizationProvider.Authenticate(loginUserDto);
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
