using System.Windows;
using System.Windows.Controls;

namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get(); //NOT SURE
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowManagerChoiceButton_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new Order.OrderListWindow().Show();
        private void ShowCartsButton_Click(object sender, RoutedEventArgs e) => new Cart.CartListWindow().Show();

    }
}
