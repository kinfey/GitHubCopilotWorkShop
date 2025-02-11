using Xunit;
using MinimalAPI;

public class OrderTests
{
    private readonly IOrderService _orderService;

    public OrderTests()
    {
        _orderService = new OrderService();
    }

    [Fact]
    public void GetMenu_ReturnsMenu()
    {
        var menu = _orderService.GetMenu();

        Assert.NotNull(menu);
        Assert.NotEmpty(menu);
    }

    [Fact]
    public void CreateOrder_ValidOrder_ReturnsTrue()
    {
        var order = new Order
        {
            Id = 1,
            Product = "Test Product",
            Quantity = 1,
            DeliveryMethod = "Delivery"
        };

        var result = _orderService.CreateOrder(order);

        Assert.True(result);
    }

    [Fact]
    public void CreateOrder_InvalidOrder_ReturnsFalse()
    {
        var order = new Order
        {
            Id = 1,
            Product = "",
            Quantity = 1,
            DeliveryMethod = "Delivery"
        };

        var result = _orderService.CreateOrder(order);

        Assert.False(result);
    }

    [Fact]
    public void ConfirmOrder_ValidOrderId_ReturnsTrue()
    {
        var orderId = 1;

        var result = _orderService.ConfirmOrder(orderId);

        Assert.True(result);
    }

    [Fact]
    public void ConfirmOrder_InvalidOrderId_ReturnsFalse()
    {
        var orderId = -1;

        var result = _orderService.ConfirmOrder(orderId);

        Assert.False(result);
    }

    [Fact]
    public void GetOrderStatus_ValidOrderId_ReturnsStatus()
    {
        var orderId = 1;

        var status = _orderService.GetOrderStatus(orderId);

        Assert.NotNull(status);
        Assert.NotEmpty(status);
    }

    [Fact]
    public void GetOrderStatus_InvalidOrderId_ReturnsNull()
    {
        var orderId = -1;

        var status = _orderService.GetOrderStatus(orderId);

        Assert.Null(status);
    }
}
