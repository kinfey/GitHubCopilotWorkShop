using ChengFenStore.Models;
using ChengFenStore.Data;

namespace ChengFenStore.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(Order order)
        {
            _orderRepository.AddOrder(order);
            return order;
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Order ConfirmOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order != null)
            {
                order.OrderStatus = "已支付";
                _orderRepository.UpdateOrder(order);
                // Save changes to the database
                // Assuming _orderRepository is using a DbContext, call SaveChanges() here
                // _orderRepository.SaveChanges();
            }
            return order;
        }

        public Order TrackOrder(int id)
        {
            return _orderRepository.GetOrderById(id);
        }
    }
}
