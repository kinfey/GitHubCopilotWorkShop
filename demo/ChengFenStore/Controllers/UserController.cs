using Microsoft.AspNetCore.Mvc;
using ChengFenStore.Services;
using ChengFenStore.Models;

namespace ChengFenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegistrationRequest request)
        {
            var result = _userService.Register(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var result = _userService.Login(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }
    }
}
