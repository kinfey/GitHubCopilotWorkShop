using ChengFenStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChengFenStore.Data
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        List<Payment> GetPayments();
        Payment GetPaymentById(int paymentId);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public List<Payment> GetPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }
    }
}
