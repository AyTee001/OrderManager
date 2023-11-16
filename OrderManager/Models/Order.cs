using OrderManager.Models.Enums;

namespace OrderManager.Models
{
    public class Order
    {
        public int OrderId { get; }
        public string ClientName { get; set; }
        public double Sum { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Order(int orderId, string clientName, double sum)
        {
            OrderId = orderId;
            ClientName = clientName;
            Sum = sum;
        }

        public override string ToString()
        {
            return $"Order {OrderId}:\n" +
                $"- Cutomer: {ClientName}\n" +
                $"- Sum: {Sum}\n" +
                $"- Status: {Status}";
        }
    }
}
