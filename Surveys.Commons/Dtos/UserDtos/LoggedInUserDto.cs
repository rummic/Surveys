using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Commons.Dtos.UserDtos
{
    public class LoggedInUserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
