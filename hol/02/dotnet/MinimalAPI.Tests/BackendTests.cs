using Xunit;
using MinimalAPI;

public class BackendTests
{
    private readonly IBackendService _backendService;

    public BackendTests()
    {
        _backendService = new BackendService();
    }

    [Fact]
    public void ManageUsers_ReturnsUsers()
    {
        var users = _backendService.ManageUsers();

        Assert.NotNull(users);
        Assert.NotEmpty(users);
    }

    [Fact]
    public void ManageOrders_ReturnsOrders()
    {
        var orders = _backendService.ManageOrders();

        Assert.NotNull(orders);
        Assert.NotEmpty(orders);
    }

    [Fact]
    public void ManagePayments_ReturnsPayments()
    {
        var payments = _backendService.ManagePayments();

        Assert.NotNull(payments);
        Assert.NotEmpty(payments);
    }
}
