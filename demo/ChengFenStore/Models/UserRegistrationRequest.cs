namespace ChengFenStore.Models
{
    public class UserRegistrationRequest
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
}
