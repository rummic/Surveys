using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Surveys.Api.Validators;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Commons.ErrorMessages;
using Surveys.Commons.Helpers;
using Surveys.Domain;
using Surveys.Domain.Entities;

namespace Surveys.Tests.UserTests
{
    public class RegisterUserTests
    {


        [Test]
        public void EmptyPassword_ShouldReturnEmptyPasswordError()
        {
            //Arrange
            var userData = new RegisterUserDto
            {
                Password = "",
                Name = "Test",
                Email = "test@test.com"
            };

            //Act
            var validatorResult = UserValidator.ValidateRegisterUser(userData);

            //Assert
            Assert.AreEqual(UserErrorMessages.EmptyPassword, validatorResult.ErrorMessage);
        }

        [Test]
        public void EmptyName_ShouldReturnEmptyPasswordError()
        {
            //Arrange
            var userData = new RegisterUserDto
            {
                Password = "password",
                Name = "",
                Email = "test@test.com"
            };

            //Act
            var validatorResult = UserValidator.ValidateRegisterUser(userData);

            //Assert
            Assert.AreEqual(UserErrorMessages.EmptyName, validatorResult.ErrorMessage);
        }

        [Test]
        public void EmptyEmail_ShouldReturnEmptyPasswordError()
        {
            //Arrange
            var userData = new RegisterUserDto
            {
                Password = "password",
                Name = "Test",
                Email = ""
            };

            //Act
            var validatorResult = UserValidator.ValidateRegisterUser(userData);

            //Assert
            Assert.AreEqual(UserErrorMessages.EmptyEmail, validatorResult.ErrorMessage);
        }

        [Test]
        public async Task ValidUserData_ShouldRegisterNewUser()
        {
            //Arrange
            var userData = new RegisterUserDto
            {
                Password = "password",
                Name = "Test",
                Email = "test@test.com"
            };
            IRepository repository = new InMemoryDbContextFactory().GetDbContext();
            var newUser = new User(userData);

            //Act
            await newUser.Register(repository);
            var actual = await repository.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email);

            //Assert
            Assert.AreEqual(newUser.Email, actual.Email);
            Assert.AreEqual(newUser.Password, actual.Password);
            Assert.AreEqual(newUser.Name, actual.Name);
        }

        [Test]
        public async Task DuplicateUserData_ShouldReturnDuplicateEmailError()
        {
            //Arrange
            var userData = new RegisterUserDto
            {
                Password = "password",
                Name = "Test",
                Email = "test@test.com"
            };
            IRepository repository = new InMemoryDbContextFactory().GetDbContext();
            var newUser = new User(userData);
            await newUser.Register(repository);
            var duplicateUser = new User(userData);

            //Act
            var result = await duplicateUser.Register(repository);

            //Assert
            Assert.AreEqual(UserErrorMessages.DuplicateEmail, result.ErrorMessage);
        }
    }
}
