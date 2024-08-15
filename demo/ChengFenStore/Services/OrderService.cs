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
            return _orderRepository.AddOrder(order);
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
            }
            return order;
        }

        public Order TrackOrder(int id)
        {
            return _orderRepository.GetOrderById(id);
        }
    }
}
