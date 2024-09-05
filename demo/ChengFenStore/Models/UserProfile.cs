namespace ChengFenStore.Models
{
    public class UserProfile
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
