using OrderManager.ViewModels.Abstract;
using System.Collections.Generic;
using System;
using OrderManager.Models.Enums;
using System.Linq;
using OrderManager.Models;
using System.Windows.Input;
using OrderManager.Commands;

namespace OrderManager.ViewModels
{
    public class OrderManagerVM : ViewModelBase
    {
        private readonly OrderManagerModel _manager = new();

        private int? _sum = null;

        private int? _orderIdStatusChange = null;

        private int? _orderIdDeletion = null;

        private string _clientName = string.Empty;

        private OrderStatus _status;
        public ICommand Delete { get; }
        public ICommand ChangeStatus { get; }
        public ICommand Record { get; }
        public ICommand Download { get; }

        private string _deletionResult = string.Empty;

        private string _updateResult = string.Empty;

        public ICollection<Order> Orders
        {
            get => _manager.Orders;
        }

        public int? Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        public int? OrderIdStatusChange
        {
            get => _orderIdStatusChange;
            set
            {
                _orderIdStatusChange = value;
                OnPropertyChanged(nameof(OrderIdStatusChange));
            }
        }

        public int? OrderIdDeletion
        {
            get => _orderIdDeletion;
            set
            {
                _orderIdDeletion = value;
                OnPropertyChanged(nameof(OrderIdDeletion));
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }

        public string DeletionResult
        {
            get => _deletionResult;
            set
            {
                _deletionResult = value;
                OnPropertyChanged(nameof(DeletionResult));
            }
        }

        public string UpdateResult
        {
            get => _updateResult;
            set
            {
                _updateResult = value;
                OnPropertyChanged(nameof(UpdateResult));
            }
        }

        public string Status
        {
            get => _status.ToString();
            set
            {
                if (Enum.TryParse(value, out OrderStatus result))
                {
                    _status = result;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }


        public List<string> OrderStatusOptions { get; } = Enum.GetNames(typeof(OrderStatus)).ToList();

        public OrderManagerVM()
        {
            Record = new RelayCommand(
                executeAction  => {
                    _manager.MakeNewOrder(_clientName, (int)_sum!);
                },
                canExecuteFunc => true);

            ChangeStatus = new RelayCommand(
                executeAction =>
                {
                    UpdateResult = _manager.TryChangeOrderStatus((int)_orderIdStatusChange!, _status) ? "Success" : "Order not found";
                },
                canExecuteFunc => true);

            Delete = new RelayCommand(
                executeAction =>
                {
                    DeletionResult = _manager.TryRemoveOrder((int)_orderIdDeletion!) ? "Success" : "Order not found";
                },
                canExecuteFunc => true);

            Download = new RelayCommand(
                executeAction => _manager.DownloadOrders(),
                canExecuteFunc => true);
        }
    }
}
