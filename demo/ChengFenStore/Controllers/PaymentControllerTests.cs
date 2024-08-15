using ChengFenStore.Controllers;
using ChengFenStore.Models;
using ChengFenStore.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChengFenStore.Tests
{
    public class PaymentControllerTests
    {
        private readonly Mock<PaymentService> _mockPaymentService;
        private readonly PaymentController _controller;

        public PaymentControllerTests()
        {
            _mockPaymentService = new Mock<PaymentService>();
            _controller = new PaymentController(_mockPaymentService.Object);
        }

        [Fact]
        public void ProcessPayment_ReturnsOkResult_WhenPaymentIsProcessed()
        {
            // Arrange
            var payment = new Payment { PaymentId = 1, OrderId = 1, PaymentMethod = "CreditCard", PaymentStatus = "Pending" };
            _mockPaymentService.Setup(service => service.ProcessPayment(payment)).Returns(true);

            // Act
            var result = _controller.ProcessPayment(payment);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Payment processed successfully", ((dynamic)okResult.Value).message);
        }

        [Fact]
        public void ProcessPayment_ReturnsBadRequest_WhenPaymentProcessingFails()
        {
            // Arrange
            var payment = new Payment { PaymentId = 1, OrderId = 1, PaymentMethod = "CreditCard", PaymentStatus = "Pending" };
            _mockPaymentService.Setup(service => service.ProcessPayment(payment)).Returns(false);

            // Act
            var result = _controller.ProcessPayment(payment);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Payment processing failed", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void GetPaymentRecords_ReturnsOkResult_WithPaymentRecords()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new Payment { PaymentId = 1, OrderId = 1, PaymentMethod = "CreditCard", PaymentStatus = "Processed" },
                new Payment { PaymentId = 2, OrderId = 2, PaymentMethod = "PayPal", PaymentStatus = "Processed" }
            };
            _mockPaymentService.Setup(service => service.GetPaymentRecords()).Returns(payments);

            // Act
            var result = _controller.GetPaymentRecords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPayments = Assert.IsType<List<Payment>>(okResult.Value);
            Assert.Equal(2, returnedPayments.Count);
        }
    }
}
