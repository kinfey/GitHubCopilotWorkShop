namespace ChengFenStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string ProductDetails { get; set; }
        public string DeliveryMethod { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderStatus> OrderStatusHistory { get; set; }

        public Order()
        {
            OrderStatusHistory = new List<OrderStatus>();
        }
    }
}
