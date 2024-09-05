using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChengFenStore.Data;
using ChengFenStore.Models;
using Microsoft.IdentityModel.Tokens;

namespace ChengFenStore.Services
{
    public interface IUserService
    {
        UserRegistrationResponse Register(UserRegistrationRequest request);
        UserLoginResponse Login(UserLoginRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserRegistrationResponse Register(UserRegistrationRequest request)
        {
            // Check if user already exists
            var existingUser = _userRepository.GetUserByPhoneNumber(request.PhoneNumber);
            if (existingUser != null)
            {
                return new UserRegistrationResponse
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            // Create new user
            var user = new User
            {
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Password = request.Password // Note: In a real application, you should hash the password before storing it
            };

            _userRepository.AddUser(user);

            return new UserRegistrationResponse
            {
                Success = true,
                Message = "User registered successfully."
            };
        }

        public UserLoginResponse Login(UserLoginRequest request)
        {
            // Get user by phone number
            var user = _userRepository.GetUserByPhoneNumber(request.PhoneNumber);
            if (user == null || user.Password != request.Password) // Note: In a real application, you should hash the password and compare the hashes
            {
                return new UserLoginResponse
                {
                    Success = false,
                    Message = "Invalid phone number or password."
                };
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return new UserLoginResponse
            {
                Success = true,
                Message = "Login successful.",
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.PhoneNumber)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
