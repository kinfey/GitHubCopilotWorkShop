using ChengFenStore.Models;
using ChengFenStore.Data;

namespace ChengFenStore.Services
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool ProcessPayment(Payment payment)
        {
            // Implement payment processing logic here
            // For example, call external payment gateway API and update payment status
            payment.PaymentStatus = "Processed";
            _paymentRepository.AddPayment(payment);
            return true;
        }

        public List<Payment> GetPaymentRecords()
        {
            return _paymentRepository.GetPayments();
        }
    }
}
