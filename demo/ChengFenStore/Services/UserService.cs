using ChengFenStore.Data;
using ChengFenStore.Models;

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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            return new UserLoginResponse
            {
                Success = true,
                Message = "Login successful."
            };
        }
    }
}
