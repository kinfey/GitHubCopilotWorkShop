namespace ChengFenStore.Models
{
    public class UserLoginRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
}
