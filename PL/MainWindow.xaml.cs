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
        private void ShowFollowOrdersButton_Click(object sender, RoutedEventArgs e) => new Order.FollowOrderPopUpWindow().Show();
        private void ShowCatalogButton_Click(object sender, RoutedEventArgs e) => new Cart.CatalogWindow().Show();
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
