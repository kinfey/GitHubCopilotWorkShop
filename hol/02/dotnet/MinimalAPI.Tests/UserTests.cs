using Xunit;
using MinimalAPI;

public class UserTests
{
    private readonly IUserService _userService;

    public UserTests()
    {
        _userService = new UserService();
    }

    [Fact]
    public void Register_ValidUser_ReturnsTrue()
    {
        var user = new User
        {
            PhoneNumber = "1234567890",
            Name = "Test User",
            Password = "password"
        };

        var result = _userService.Register(user);

        Assert.True(result);
    }

    [Fact]
    public void Register_InvalidUser_ReturnsFalse()
    {
        var user = new User
        {
            PhoneNumber = "",
            Name = "Test User",
            Password = "password"
        };

        var result = _userService.Register(user);

        Assert.False(result);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsTrue()
    {
        var loginRequest = new LoginRequest
        {
            PhoneNumber = "1234567890",
            Password = "password"
        };

        var result = _userService.Login(loginRequest);

        Assert.True(result);
    }

    [Fact]
    public void Login_InvalidCredentials_ReturnsFalse()
    {
        var loginRequest = new LoginRequest
        {
            PhoneNumber = "1234567890",
            Password = "wrongpassword"
        };

        var result = _userService.Login(loginRequest);

        Assert.False(result);
    }

    [Fact]
    public void Verify_ValidPhoneNumber_ReturnsTrue()
    {
        var phoneNumber = "1234567890";

        var result = _userService.Verify(phoneNumber);

        Assert.True(result);
    }

    [Fact]
    public void Verify_InvalidPhoneNumber_ReturnsFalse()
    {
        var phoneNumber = "";

        var result = _userService.Verify(phoneNumber);

        Assert.False(result);
    }
}
