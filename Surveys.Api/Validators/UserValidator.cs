using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;

namespace Surveys.Api.Validators
{
    public static class UserValidator
    {
        public static Result ValidateRegisterUser(RegisterUserDto userData)
        {
            var result = new Result();
            
            if (string.IsNullOrEmpty(userData.Email))
            {
                result.ErrorMessage = "Email cannot be empty.";
                return result;
            }
            if (string.IsNullOrEmpty(userData.Name))
            {
                result.ErrorMessage = "Name cannot be empty.";
                return result;
            }
            if (string.IsNullOrEmpty(userData.Password))
            {
                result.ErrorMessage = "Password cannot be empty.";
                return result;
            }

            return result;
        }
    }
}
