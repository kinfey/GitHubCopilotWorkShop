using ChengFenStore.Controllers;
using ChengFenStore.Models;
using ChengFenStore.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChengFenStore.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Fact]
        public void Register_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new UserRegistrationRequest
            {
                PhoneNumber = "1234567890",
                Name = "Test User",
                Password = "password"
            };
            var response = new UserRegistrationResponse
            {
                Success = true,
                Message = "User registered successfully."
            };
            _mockUserService.Setup(service => service.Register(request)).Returns(response);

            // Act
            var result = _userController.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public void Register_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserRegistrationRequest
            {
                PhoneNumber = "1234567890",
                Name = "Test User",
                Password = "password"
            };
            var response = new UserRegistrationResponse
            {
                Success = false,
                Message = "User already exists."
            };
            _mockUserService.Setup(service => service.Register(request)).Returns(response);

            // Act
            var result = _userController.Register(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(response, badRequestResult.Value);
        }

        [Fact]
        public void Login_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                PhoneNumber = "1234567890",
                Password = "password"
            };
            var response = new UserLoginResponse
            {
                Success = true,
                Message = "Login successful."
            };
            _mockUserService.Setup(service => service.Login(request)).Returns(response);

            // Act
            var result = _userController.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public void Login_InvalidRequest_ReturnsUnauthorizedResult()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                PhoneNumber = "1234567890",
                Password = "password"
            };
            var response = new UserLoginResponse
            {
                Success = false,
                Message = "Invalid phone number or password."
            };
            _mockUserService.Setup(service => service.Login(request)).Returns(response);

            // Act
            var result = _userController.Login(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(response, unauthorizedResult.Value);
        }
    }
}
