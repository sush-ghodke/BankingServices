using Microsoft.Extensions.Logging;
using Moq;
using UserModule.Controllers;
using UserModule.Models;
using UserModule.Service;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserModule.Controllers;
using UserModule.ApiResponse;
using System;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Net;



namespace UserModule.Test
{
    public class UserModuleUnitTest
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public Mock<IUserService> mock = new Mock<IUserService>();

        [Fact]
        public async Task GetUser_ReturnsOk_WithUsers()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var result = await controller.GetUser();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);
            Assert.Equal(Constants.Success, response.Status);
            Assert.Equal(Constants.Get, response.Message);
            Assert.Single((IEnumerable<Users>)response.Data);

           
        }
        [Fact]
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

      
        [Fact]
        public async Task GetUserById_ReturnsOk_WithValidId()
        {

            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetUserById(0);

            Assert.Null(result.Value);          

        }

        [Fact]
        public async Task GetUser_ById_Success()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            userServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(new Users { UserId =1, FirstName ="Ram", LastName ="Sharma", Gender='M',
                Address="ABCD",Phone=1234567890,Email="ram123@gmail.com",PAN="ASD123",UID="AWEE123",Status="Active",Username="ram",Password="ram123",CreatedDate=DateTime.Now,
                ModifyDate = DateTime.Now});
                
            var result = await controller.GetUserById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);
            Assert.Equal(Constants.Success, response.Status);
            Assert.Equal(Constants.Get, response.Message);


        }

        [Fact]
        public async Task GetUser_ById_Else()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

            // Act
            userServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(() => null);

            var result = await controller.GetUserById(1);
            Assert.Null(result.Value);


        }

        [Fact]
        public async Task AddUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);

           

            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Add(users)).ReturnsAsync(new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            });

            var result = await controller.AddUser(users);
            Assert.NotNull(result);
           
         }

        [Fact]
        public async Task AddUser_IfCondition()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Add(users)).ReturnsAsync(()=>null
                );

            var result = await controller.AddUser(users);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task UpdateUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Update(1,users)).ReturnsAsync(new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            });

            var result = await controller.UpdateUser(1,users);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task UpdateUser_Else()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            // Act
            userServiceMock.Setup(service => service.Update(1, users)).ReturnsAsync(() => null);
                

            var result = await controller.UpdateUser(1, users);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task UpdateUser_Else2()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Update(1, users)).ReturnsAsync(() => null);


            var result = await controller.UpdateUser(2, users);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task DeleteUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            //userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(new List<Users> { new Users() });

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



           
            // Act

            var result = await controller.DeleteUser(0);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task DeleteUser_Else()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Delete(1)).ReturnsAsync(new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            });

            var result = await controller.DeleteUser(1);
            Assert.Null(result.Value);

        }

        [Fact]
        public async Task DeleteUser_Else2()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(userServiceMock.Object, loggerMock.Object);



            Users users = new Users
            {
                UserId = 1,
                FirstName = "Ram",
                LastName = "Sharma",
                Gender = 'M',
                Address = "ABCD",
                Phone = 1234567890,
                Email = "ram123@gmail.com",
                PAN = "ASD123",
                UID = "AWEE123",
                Status = "Active",
                Username = "ram",
                Password = "ram123",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            // Act
            userServiceMock.Setup(service => service.Delete(1)).ReturnsAsync(() => null);

            var result = await controller.DeleteUser(1);
            Assert.Null(result.Value);

        }
    }
}