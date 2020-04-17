using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Commons.ErrorMessages;
using Surveys.Commons.Helpers;

namespace Surveys.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

        private User()
        {

        }
        public User(RegisterUserDto userData)
        {
            Id = Guid.NewGuid();
            Name = userData.Name;
            Email = userData.Email;
            Salt = PasswordHelper.CreateSalt();
            Password = PasswordHelper.HashPassword(userData.Password, Salt);
            Role = Role.User;
        }


        public async Task<Result> Register(IRepository repository)
        {
            var result = new Result();
            var userFromDb = await repository.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (userFromDb != null)
            {
                result.ErrorMessage = UserErrorMessages.DuplicateEmail;
                return result;
            }

            await repository.Users.AddAsync(this);
            await repository.SaveChangesAsync();
            return result;
        }

    }

    public enum Role
    {
        Admin,
        User
    }
}
