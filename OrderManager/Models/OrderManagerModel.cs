using OrderManager.Models.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace OrderManager.Models
{
    public class OrderManagerModel : INotifyPropertyChanged
    {
        private int _orderGlobalId;

        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            private set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        public OrderManagerModel()
        {
            _orders = new ObservableCollection<Order>();
        }

        public void MakeNewOrder(string clientName, int sum)
        {
            var order = new Order(++_orderGlobalId, clientName, sum);
            Orders.Add(order);
        }

        public bool TryRemoveOrder(int orderId)
        {
            var order = Orders.FirstOrDefault(x => x.OrderId == orderId);
            if (order != null)
            {
                Orders.Remove(order);
                return true;
            }
            return false;
        }

        public bool TryChangeOrderStatus(int orderId, OrderStatus status)
        {
            var existingOrder = Orders.FirstOrDefault(x => x.OrderId == orderId);

            if (existingOrder is not null)
            {
                var updatedOrder = new Order(orderId, existingOrder.ClientName, existingOrder.Sum)
                {
                    Status = status
                };

                var index = Orders.IndexOf(existingOrder);
                Orders[index] = updatedOrder;
                return true;
            }
            return false;
        }

        public void DownloadOrders()
        {
            DriveInfo dDrive = new ("D:");
            string filePath = Path.Combine(dDrive.RootDirectory.FullName, "orders.txt");
            using StreamWriter writer = new(filePath);
            writer.Write(string.Join("\n", Orders));
        }
    }
}
