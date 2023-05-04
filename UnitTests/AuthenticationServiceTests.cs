using Microsoft.Extensions.Configuration;
using WebAPI.Repository.Interfaces;
using WebAPI.Services;

namespace UnitTests
{
    public class AuthenticationServiceTests
    {
        private IAuthenticationService _authenticationService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _configurationMock = new Mock<IConfiguration>();
            _authenticationService = new AuthenticationService(_unitOfWorkMock.Object, _configurationMock.Object);
        }


    }
}
