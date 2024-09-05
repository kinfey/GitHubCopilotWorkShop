namespace ChengFenStore.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public List<PaymentStatus> PaymentStatusHistory { get; set; }

        public Payment()
        {
            PaymentStatusHistory = new List<PaymentStatus>();
        }
    }
}
