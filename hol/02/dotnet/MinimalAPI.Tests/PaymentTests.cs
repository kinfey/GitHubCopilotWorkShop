using Xunit;
using MinimalAPI;

public class PaymentTests
{
    private readonly IPaymentService _paymentService;

    public PaymentTests()
    {
        _paymentService = new PaymentService();
    }

    [Fact]
    public void ProcessPayment_ValidPayment_ReturnsTrue()
    {
        var payment = new Payment
        {
            OrderId = 1,
            PaymentMethod = "CreditCard",
            Amount = 100.00m
        };

        var result = _paymentService.ProcessPayment(payment);

        Assert.True(result);
    }

    [Fact]
    public void ProcessPayment_InvalidPayment_ReturnsFalse()
    {
        var payment = new Payment
        {
            OrderId = -1,
            PaymentMethod = "CreditCard",
            Amount = 100.00m
        };

        var result = _paymentService.ProcessPayment(payment);

        Assert.False(result);
    }

    [Fact]
    public void GetPaymentRecords_ReturnsRecords()
    {
        var records = _paymentService.GetPaymentRecords();

        Assert.NotNull(records);
        Assert.NotEmpty(records);
    }
}
