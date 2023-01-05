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
        private void ShowManagerOptionsButton_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new Order.FollowOrderWindow().Show();
        private void ShowCatalogButton_Click(object sender, RoutedEventArgs e) => new Cart.CatalogWindow().Show();

    }
}
