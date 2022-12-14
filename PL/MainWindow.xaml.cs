using BlApi;
using System.Windows;


namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = new BlImplementation.Bl(); //NOT SURE
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new Product.ProductListWindow().Show();
    }
}
