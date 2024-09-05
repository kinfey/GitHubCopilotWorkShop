using ChengFenStore.Models;
using ChengFenStore.Data;

namespace ChengFenStore.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public bool ProcessPayment(Payment payment)
        {
            // Implement payment processing logic here
            // For example, call external payment gateway API and update payment status
            payment.PaymentStatus = "Processed";
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return true;
        }

        public List<Payment> GetPaymentRecords()
        {
            return _context.Payments.ToList();
        }
    }
}
