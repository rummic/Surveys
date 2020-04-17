using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Surveys.Api;
using Surveys.Commons.Dtos.UserDtos;
using Surveys.Commons.ErrorMessages;
using Surveys.Domain;
using Surveys.Domain.Entities;

namespace Surveys.Tests.UserTests
{
    public class AuthenticateUserTests
    {
        [Test]
        public async Task InvalidEmail_ShouldReturnNotFoundByEmailError()
        {
            //Arrange
            IRepository repository = new InMemoryDbContextFactory().GetDbContext();
            var myConfiguration = new Dictionary<string, string>
            {
                {"Secret", "SUPERSECRETTESTSTRING"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var authorizationProvider = new AuthorizationProvider(repository, configuration);
            var userInDb = new User(new RegisterUserDto
            {
                Email = "test@test.com",
                Name = "test",
                Password = "password"
            });
            await userInDb.Register(repository);
            var userLogin = new LoginUserDto
            {
                Email = "test",
                Password = "password"
            };


            //Act
            var result = await authorizationProvider.Authenticate(userLogin);


            //Assert
            Assert.AreEqual(UserErrorMessages.NotFoundByEmail,result.ErrorMessage);
        }

        [Test]
        public async Task InvalidPassword_ShouldReturnInvalidPasswordError()
        {
            //Arrange
            IRepository repository = new InMemoryDbContextFactory().GetDbContext();
            var myConfiguration = new Dictionary<string, string>
            {
                {"Secret", "SUPERSECRETTESTSTRING"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var authorizationProvider = new AuthorizationProvider(repository, configuration);
            var userInDb = new User(new RegisterUserDto
            {
                Email = "test@test.com",
                Name = "test",
                Password = "password"
            });
            await userInDb.Register(repository);
            var userLogin = new LoginUserDto
            {
                Email = "test@test.com",
                Password = "wrongpassword"
            };


            //Act
            var result = await authorizationProvider.Authenticate(userLogin);


            //Assert
            Assert.AreEqual(UserErrorMessages.InvalidPassword, result.ErrorMessage);
        }


        [Test]
        public async Task ValidPassword_ShouldReturnLoggedInUser()
        {
            //Arrange
            IRepository repository = new InMemoryDbContextFactory().GetDbContext();
            var myConfiguration = new Dictionary<string, string>
            {
                {"Secret", "SUPERSECRETTESTSTRING"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var authorizationProvider = new AuthorizationProvider(repository, configuration);
            var userInDb = new User(new RegisterUserDto
            {
                Email = "test@test.com",
                Name = "test",
                Password = "password"
            });
            await userInDb.Register(repository);
            var userLogin = new LoginUserDto
            {
                Email = "test@test.com",
                Password = "password"
            };


            //Act
            var result = await authorizationProvider.Authenticate(userLogin);


            //Assert
            Assert.AreEqual(userInDb.Email, result.Value.Email);
            Assert.AreEqual(userInDb.Role.ToString(), result.Value.Role);
            Assert.IsNotEmpty(result.Value.Token);
        }
    }
}
