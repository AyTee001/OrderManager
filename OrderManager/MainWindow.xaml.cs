using OrderManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OrderManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new OrderManagerVM();

            NameBox.TextChanged += OrderButton_TextChanged;
            SumBox.TextChanged += OrderButton_TextChanged;
            OrderIdDelBox.TextChanged += DelButton_TextChanged;
            OrderIdUpdateBox.TextChanged += ChangeStatusButton_TextChanged;
        }

        private void OrderButton_TextChanged(object sender, RoutedEventArgs e)
        {
            OrderButton.IsEnabled = !Validation.GetHasError(NameBox) && !Validation.GetHasError(SumBox) && NameBox.Text.Length > 0 && SumBox.Text.Length > 0;
        }

        private void DelButton_TextChanged(object sender, RoutedEventArgs e)
        {
            DelButton.IsEnabled = !Validation.GetHasError(OrderIdDelBox) && OrderIdDelBox.Text.Length > 0;
        }

        private void ChangeStatusButton_TextChanged(object sender, RoutedEventArgs e)
        {
            ChangeStatusButton.IsEnabled = !Validation.GetHasError(OrderIdUpdateBox) && OrderIdUpdateBox.Text.Length > 0;
        }

    }
}
