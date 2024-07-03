using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserModule.ApiResponse;
using UserModule.Controllers;
using UserModule.Models;
using UserModule.Service;
namespace TestProject1
{
    public class Tests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UserController>> _loggerMock;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetUser_WithUsers_ReturnsOk()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            _userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var result = await _controller.GetUser();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var apiResponse = okResult.Value as ApiResponse<dynamic>;
            Assert.AreEqual(Constants.Success, apiResponse.Status);
            Assert.AreEqual(Constants.Get, apiResponse.Message);
        }

        [Test]
        public async Task GetUser_WithNoUsers_ReturnsNotFound()
        {
            // Arrange
            var mockUsers = new List<Users>(); // Empty list

            _userServiceMock.Setup(x => x.GetAll()).ReturnsAsync(mockUsers);

            // Act
            var result = await _controller.GetUser();

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            var apiResponse = notFoundResult.Value as ApiResponse<dynamic>;
            Assert.AreEqual(Constants.Badrequest, apiResponse.Status);
            Assert.AreEqual(Constants.Failure, apiResponse.Message);
        }

        [Test]
        public async Task GetAccountDetailByid_WithValidAccountId_ReturnsOk()
        {
            _userServiceMock = new Mock<IUserService>();
            User user = new User();
            // Arrange
            int UserId = 123;
            var User = new User
            {
                UserId = 1,
                FirstName = "Test1F",
                Email = "Test2gmail.com,",
                Pan = "pannumber"

            };
            //  _userServiceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<User> { user });

        }

        [Test]
        public async Task GetUser_ReturnsNotFound_WithEmptyUsers()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users>());

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetUser();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse<dynamic>>(notFoundResult.Value);
            Assert.Equal(Constants.Badrequest, response.Status);
            Assert.Equal(Constants.Failure, response.Message);

        }



    }
}