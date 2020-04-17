using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Commons.ErrorMessages
{
    public static class UserErrorMessages
    {
        public const string EmptyPassword = "Password cannot be empty.";
        public const string EmptyName = "Name cannot be empty.";
        public const string EmptyEmail = "Email cannot be empty.";
        public const string DuplicateEmail = "User with provided email is already registered.";
    }
}
