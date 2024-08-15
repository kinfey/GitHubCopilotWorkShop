using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ChengFenStore.Controllers;
using ChengFenStore.Services;
using ChengFenStore.Models;

namespace ChengFenStore.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<OrderService> _mockOrderService;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<OrderService>();
            _orderController = new OrderController(_mockOrderService.Object);
        }

        [Fact]
        public void CreateOrder_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, ProductDetails = "Product 1", DeliveryMethod = "Delivery", OrderStatus = "Pending" };
            _mockOrderService.Setup(service => service.CreateOrder(order)).Returns(order);

            // Act
            var result = _orderController.CreateOrder(order);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetOrderById", createdAtActionResult.ActionName);
            Assert.Equal(order.OrderId, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(order, createdAtActionResult.Value);
        }

        [Fact]
        public void GetOrderById_ReturnsOkObjectResult_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, ProductDetails = "Product 1", DeliveryMethod = "Delivery", OrderStatus = "Pending" };
            _mockOrderService.Setup(service => service.GetOrderById(order.OrderId)).Returns(order);

            // Act
            var result = _orderController.GetOrderById(order.OrderId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order, okObjectResult.Value);
        }

        [Fact]
        public void GetOrderById_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderService.Setup(service => service.GetOrderById(It.IsAny<int>())).Returns((Order)null);

            // Act
            var result = _orderController.GetOrderById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ConfirmOrder_ReturnsOkObjectResult_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, ProductDetails = "Product 1", DeliveryMethod = "Delivery", OrderStatus = "Pending" };
            _mockOrderService.Setup(service => service.ConfirmOrder(order.OrderId)).Returns(order);

            // Act
            var result = _orderController.ConfirmOrder(order.OrderId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order, okObjectResult.Value);
        }

        [Fact]
        public void ConfirmOrder_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderService.Setup(service => service.ConfirmOrder(It.IsAny<int>())).Returns((Order)null);

            // Act
            var result = _orderController.ConfirmOrder(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void TrackOrder_ReturnsOkObjectResult_WhenOrderExists()
        {
            // Arrange
            var order = new Order { OrderId = 1, UserId = 1, ProductDetails = "Product 1", DeliveryMethod = "Delivery", OrderStatus = "Pending" };
            _mockOrderService.Setup(service => service.TrackOrder(order.OrderId)).Returns(order);

            // Act
            var result = _orderController.TrackOrder(order.OrderId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order, okObjectResult.Value);
        }

        [Fact]
        public void TrackOrder_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockOrderService.Setup(service => service.TrackOrder(It.IsAny<int>())).Returns((Order)null);

            // Act
            var result = _orderController.TrackOrder(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
