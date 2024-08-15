using ChengFenStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChengFenStore.Data
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = GetOrderById(order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.UserId = order.UserId;
                existingOrder.ProductDetails = order.ProductDetails;
                existingOrder.DeliveryMethod = order.DeliveryMethod;
                existingOrder.OrderStatus = order.OrderStatus;
            }
        }
    }
}
