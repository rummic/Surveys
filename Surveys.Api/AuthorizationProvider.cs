using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Surveys.Api.Validators;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Domain;
using Surveys.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Api
{
    public class AuthorizationProvider
    {
        private readonly IRepository _repository;
        public IConfiguration Configuration { get; }

        public AuthorizationProvider(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }

        public async Task<Result<LoggedInUserDto>> Authenticate(LoginUserDto loginUserDto)
        {
            var result = new Result<LoggedInUserDto>();

            var userResult = await new User().GetByEmail(loginUserDto.Email, _repository);
            if (userResult.HasError)
            {
                result.ErrorMessage = userResult.ErrorMessage;
                return result;
            }

            Result<LoggedInUserDto> validatorResult = UserValidator.ValidateAuthenticate(userResult.Value, loginUserDto);
            if (validatorResult.HasError)
            {
                result.ErrorMessage = validatorResult.ErrorMessage;
                return result;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));

            var subject = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, userResult.Value.Email), new Claim(ClaimTypes.Role, userResult.Value.Role.ToString()) });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Value = new LoggedInUserDto()
            {
                Email = userResult.Value.Email,
                Role = userResult.Value.Role.ToString(),
                Token = tokenHandler.WriteToken(token)
            };
            return result;
        }
    }
}
