﻿using Microsoft.Extensions.Configuration;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;
using WebAPI.Services;

namespace UnitTests
{
    public class AuthenticationServiceTests
    {
        private IAuthenticationService _authenticationService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IConfiguration> _configurationMock;

        private UserLoginDTO _validUser;
        private User _user;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _configurationMock = new Mock<IConfiguration>();

            _validUser = new UserLoginDTO()
            {
                EmailAddress = "email@address.com",
                Password = "P@ssw0rd"
            };

            _user = new User()
            {
                EmailAddress = _validUser.EmailAddress,
                Role = RoleType.user
            };

            _unitOfWorkMock.Setup(x => x.Users.GetUserByEmail(_validUser.EmailAddress)).ReturnsAsync(_user);

            _authenticationService = new AuthenticationService(_unitOfWorkMock.Object, _configurationMock.Object);
        }

        [Test]
        public void AuthenticateUser_NullCredentials_ShouldThrowError()
        {
            var invalidUser = new UserLoginDTO()
            {
                EmailAddress = "",
                Password = ""
            };

            var exception = Assert.ThrowsAsync<Exception>(async () => await _authenticationService.Authenticate(invalidUser) );
            Assert.That(exception.Message, Is.EqualTo("Must enter a email and password"));
        }

        [Test]
        public void AuthenticateUser_UserDoesntExist_ShouldThrowError()
        {
            var invalidUser = new UserLoginDTO()
            {
                EmailAddress = "invalid@user.com",
                Password = "P@ssw0rd"
            };

            var exception = Assert.ThrowsAsync<Exception>(async () => await _authenticationService.Authenticate(invalidUser));
            Assert.That(exception.Message, Is.EqualTo("User doesn't exist"));
        }

        [Test]
        public void AuthenticateUser_IncorrectPassword_ShouldThrowError()
        {
            var invalidUser = new UserLoginDTO()
            {
                EmailAddress = "P@ssw0rd",
                Password = "invalid"
            };

            _user.PasswordSalt = _authenticationService.GenerateSalt();
            _user.HashedPassword = _authenticationService.HashPassword(invalidUser.Password, _user.PasswordSalt);

            var exception = Assert.ThrowsAsync<Exception>(async () => await _authenticationService.Authenticate(invalidUser));
            Assert.That(exception.Message, Is.EqualTo("User doesn't exist"));
        }

        [Test]
        public async Task AuthenticateUser_ValidCredentials_ShouldReturnToken()
        {
            _user.PasswordSalt = _authenticationService.GenerateSalt();
            _user.HashedPassword = _authenticationService.HashPassword(_validUser.Password, _user.PasswordSalt);

            _unitOfWorkMock.Setup(x => x.Users.GetUserByEmailAndHashedPassword(_validUser.EmailAddress, _user.HashedPassword)).ReturnsAsync(_user);

            _configurationMock.SetupGet(x => x["JWT:Key"]).Returns("someVeryLongSecretKey");
            _configurationMock.SetupGet(x => x["JWT:Issuer"]).Returns("issuer");
            _configurationMock.SetupGet(x => x["JWT:Audience"]).Returns("audience");

            var token = await _authenticationService.Authenticate(_validUser);
        }
    }
}