using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Commons.ErrorMessages;

namespace Surveys.Api.Validators
{
    public static class UserValidator
    {
        public static Result ValidateRegisterUser(RegisterUserDto userData)
        {
            var result = new Result();
            
            if (string.IsNullOrEmpty(userData.Email))
            {
                result.ErrorMessage = UserErrorMessages.EmptyEmail;
                return result;
            }
            if (string.IsNullOrEmpty(userData.Name))
            {
                result.ErrorMessage = UserErrorMessages.EmptyName;
                return result;
            }
            if (string.IsNullOrEmpty(userData.Password))
            {
                result.ErrorMessage = UserErrorMessages.EmptyPassword;
                return result;
            }

            return result;
        }
    }
}
