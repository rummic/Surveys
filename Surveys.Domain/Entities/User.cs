using Microsoft.EntityFrameworkCore;
using Surveys.Commons;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Commons.ErrorMessages;
using Surveys.Commons.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Surveys.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

        public User()
        {

        }

        public User(RegisterUserDto userData)
        {
            Id = Guid.NewGuid();
            Name = userData.Name;
            Email = userData.Email;
            Salt = PasswordHelper.CreateSalt();
            Password = PasswordHelper.HashPassword(userData.Password, Salt);
            Role = Entities.Role.User;
        }

        public async Task<Result<User>> GetByEmail(string email, IRepository repository)
        {
            var result = new Result<User>();
            var userFromDb = await repository.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (userFromDb == null)
            {
                result.ErrorMessage = UserErrorMessages.NotFoundByEmail;
                return result;
            }

            result.Value = userFromDb;
            return result;
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

    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
