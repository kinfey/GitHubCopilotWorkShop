using ChengFenStore.Models;

namespace ChengFenStore.Data
{
    public class PaymentRepository
    {
        private readonly List<Payment> _payments = new List<Payment>();

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
        }

        public List<Payment> GetPayments()
        {
            return _payments;
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }
    }
}
