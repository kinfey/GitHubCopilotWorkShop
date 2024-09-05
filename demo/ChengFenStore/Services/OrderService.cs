using ChengFenStore.Models;
using ChengFenStore.Data;

namespace ChengFenStore.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public Order ConfirmOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.OrderStatus = "已支付";
                _context.Orders.Update(order);
                _context.SaveChanges();
                UpdateOrderStatusHistory(order.OrderId, "已支付");
            }
            return order;
        }

        public Order TrackOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        private void UpdateOrderStatusHistory(int orderId, string status)
        {
            var orderStatus = new OrderStatus
            {
                OrderId = orderId,
                Status = status,
                Timestamp = DateTime.Now
            };

            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.OrderStatusHistory.Add(orderStatus);
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }
    }
}
