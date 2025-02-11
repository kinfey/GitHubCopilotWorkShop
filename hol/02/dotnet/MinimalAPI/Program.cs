using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IBackendService, BackendService>();

var app = builder.Build();

app.MapPost("/register", (User user, IUserService userService) =>
{
    var result = userService.Register(user);
    return result ? Results.Ok("User registered successfully") : Results.BadRequest("User registration failed");
});

app.MapPost("/login", (LoginRequest loginRequest, IUserService userService) =>
{
    var result = userService.Login(loginRequest);
    return result ? Results.Ok("Login successful") : Results.BadRequest("Login failed");
});

app.MapGet("/verify", (string phoneNumber, IUserService userService) =>
{
    var result = userService.Verify(phoneNumber);
    return result ? Results.Ok("Verification successful") : Results.BadRequest("Verification failed");
});

app.MapGet("/menu", (IOrderService orderService) =>
{
    var menu = orderService.GetMenu();
    return Results.Ok(menu);
});

app.MapPost("/createOrder", (Order order, IOrderService orderService) =>
{
    var result = orderService.CreateOrder(order);
    return result ? Results.Ok("Order created successfully") : Results.BadRequest("Order creation failed");
});

app.MapPost("/confirmOrder", (int orderId, IOrderService orderService) =>
{
    var result = orderService.ConfirmOrder(orderId);
    return result ? Results.Ok("Order confirmed successfully") : Results.BadRequest("Order confirmation failed");
});

app.MapGet("/orderStatus", (int orderId, IOrderService orderService) =>
{
    var status = orderService.GetOrderStatus(orderId);
    return Results.Ok(status);
});

app.MapPost("/processPayment", (Payment payment, IPaymentService paymentService) =>
{
    var result = paymentService.ProcessPayment(payment);
    return result ? Results.Ok("Payment processed successfully") : Results.BadRequest("Payment processing failed");
});

app.MapGet("/paymentRecords", (IPaymentService paymentService) =>
{
    var records = paymentService.GetPaymentRecords();
    return Results.Ok(records);
});

app.MapGet("/manageUsers", (IBackendService backendService) =>
{
    var users = backendService.ManageUsers();
    return Results.Ok(users);
});

app.MapGet("/manageOrders", (IBackendService backendService) =>
{
    var orders = backendService.ManageOrders();
    return Results.Ok(orders);
});

app.MapGet("/managePayments", (IBackendService backendService) =>
{
    var payments = backendService.ManagePayments();
    return Results.Ok(payments);
});

app.Run();

public interface IUserService
{
    bool Register(User user);
    bool Login(LoginRequest loginRequest);
    bool Verify(string phoneNumber);
}

public interface IOrderService
{
    List<MenuItem> GetMenu();
    bool CreateOrder(Order order);
    bool ConfirmOrder(int orderId);
    string GetOrderStatus(int orderId);
}

public interface IPaymentService
{
    bool ProcessPayment(Payment payment);
    List<PaymentRecord> GetPaymentRecords();
}

public interface IBackendService
{
    List<User> ManageUsers();
    List<Order> ManageOrders();
    List<Payment> ManagePayments();
}

public class UserService : IUserService
{
    public bool Register(User user) => true;
    public bool Login(LoginRequest loginRequest) => true;
    public bool Verify(string phoneNumber) => true;
}

public class OrderService : IOrderService
{
    public List<MenuItem> GetMenu() => new List<MenuItem>();
    public bool CreateOrder(Order order) => true;
    public bool ConfirmOrder(int orderId) => true;
    public string GetOrderStatus(int orderId) => "Order status";
}

public class PaymentService : IPaymentService
{
    public bool ProcessPayment(Payment payment) => true;
    public List<PaymentRecord> GetPaymentRecords() => new List<PaymentRecord>();
}

public class BackendService : IBackendService
{
    public List<User> ManageUsers() => new List<User>();
    public List<Order> ManageOrders() => new List<Order>();
    public List<Payment> ManagePayments() => new List<Payment>();
}

public class User
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public string DeliveryMethod { get; set; }
}

public class Payment
{
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentRecord
{
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}

public class MenuItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}
