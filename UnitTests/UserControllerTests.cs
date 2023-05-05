using WebAPI.Repository.Interfaces;
using WebAPI.Services;
using API.Controllers;
using WebAPI.Models.Entities;
using WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebAPI.Models;

namespace UnitTests
{
    public class UserControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IAuthenticationService> _authServiceMock;
        private UsersController _controller;

        private UserLoginDTO _userLogin;
        private UserRegisterDTO _userRegister;

        private readonly string _email = "address@email.com";
        private readonly string _password = "password";
        private readonly string _token = "bearerToken";
        private readonly int _id = 1;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _authServiceMock = new Mock<IAuthenticationService>();

            _userLogin = new UserLoginDTO()
            {
                EmailAddress = _email,
                Password = _password
            };

            _userRegister = new UserRegisterDTO()
            {
                EmployeeId = _id,
                EmailAddress = _email,
                Password = _password
            };

            _unitOfWorkMock.Setup(x => x.Users.GetUserByEmail(_email)).ReturnsAsync(new User() { EmailAddress = _email });
            _unitOfWorkMock.Setup(x => x.Users.GetById(_id)).ReturnsAsync(new User() { EmployeeId = _id });

            _authServiceMock.Setup(x => x.Authenticate(_userLogin)).ReturnsAsync(new Token { TokenString = _token });
            _authServiceMock.Setup(x => x.Register(_userRegister)).ReturnsAsync(new User());

            _controller = new UsersController(_unitOfWorkMock.Object, _authServiceMock.Object);
        }

        [Test]
        public async Task GetUserByEmail_UserExists_ShouldReturnUserDto()
        {
            var response = await _controller.GetUser(_email);
            var result = response as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<UserDTO>());
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task GetUserByEmail_UserDoesntExist_ShouldReturnNotFound()
        {
            _unitOfWorkMock.Setup(x => x.Users.GetUserByEmail(_email)).ReturnsAsync((User?)null);

            var response = await _controller.GetUser(_email);
            var result = response as NotFoundObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(result.Value, Is.EqualTo("User with this email doesn't exist"));
        }
        
        [Test]
        public async Task AuthenticateUser_UserWithValidCredentials_ShouldReturnToken()
        {
            var response = await _controller.Authenticate(_userLogin);
            var result = response as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.TypeOf<Token>());
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task AuthenticateUser_AuthenticationThrows_ShouldReturnBadRequest()
        {
            _authServiceMock.Setup(x => x.Authenticate(_userLogin)).ThrowsAsync(new Exception());

            var response = await _controller.Authenticate(_userLogin);
            var result = response as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public async Task AuthenticateUser_TokenIsNull_ShouldReturnUnauthorized()
        {
            _authServiceMock.Setup(x => x.Authenticate(_userLogin)).ReturnsAsync((Token?)null);

            var response = await _controller.Authenticate(_userLogin);
            var result = response as UnauthorizedResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));
        }

        [Test]
        public async Task CreateUser_ValidRequest_ShouldSaveUserInDb()
        {

            var response = await _controller.PostUser(_userRegister);
            var result = response as CreatedAtActionResult;

            _unitOfWorkMock.Verify(x => x.Users.Create(It.IsAny<User>()), Times.Once, "User was not saved in db");
            _unitOfWorkMock.Verify(x => x.Save(), Times.Once, "Changes were not saved");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        }

        [Test]
        public async Task CreateUser_RegisterThrows_ShouldReturnBadRequest()
        {
            _authServiceMock.Setup(x => x.Register(_userRegister)).ThrowsAsync(new Exception());

            var response = await _controller.PostUser(_userRegister);
            var result = response as BadRequestObjectResult;

            _unitOfWorkMock.Verify(x => x.Users.Create(It.IsAny<User>()), Times.Never, "User was saved in db");
            _unitOfWorkMock.Verify(x => x.Save(), Times.Never, "Changes were saved");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [Test]
        public async Task DeleteUser_ValidRequest_ShouldDeleteUserFromDb()
        {

            var response = await _controller.DeleteUser(_id);
            var result = response as OkResult;

            _unitOfWorkMock.Verify(x => x.Users.Delete(It.IsAny<User>()), Times.Once, "User was not deleted from db");
            _unitOfWorkMock.Verify(x => x.Save(), Times.Once, "Changes were not saved");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task DeleteUser_UserDoesntExist_ShouldDeleteUserFromDb()
        {
            _unitOfWorkMock.Setup(x => x.Users.GetById(_id)).ReturnsAsync((User?)null);

            var response = await _controller.DeleteUser(_id);
            var result = response as NotFoundObjectResult;

            _unitOfWorkMock.Verify(x => x.Users.Delete(It.IsAny<User>()), Times.Never, "User was deleted from db");
            _unitOfWorkMock.Verify(x => x.Save(), Times.Never, "Changes were saved");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(result.Value, Is.EqualTo("User with this id doesn't exist"));
        }
    }
}