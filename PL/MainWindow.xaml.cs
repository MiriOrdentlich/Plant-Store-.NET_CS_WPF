using System.Windows;

namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();


        //will be used as flag to know which options to make abailable\unavailale for user according to their status
        //isManager==0(==false) => user is client, isManager==1(==true) => user is manager 
        public int isManager
        {
            get { return (int)GetValue(isManagerProperty); }
            set { SetValue(isManagerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isManagerProperty =
            DependencyProperty.Register("isManager", typeof(int), typeof(Window), new PropertyMetadata(0));



        public BO.Cart currentCart
        {
            get { return (BO.Cart)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public MainWindow(int flag) //for explanation on flag: see notes on isManager Dependency Property  
        {
            InitializeComponent();
            isManager = flag;
        }

        private void ShowFollowOrdersButton_Click(object sender, RoutedEventArgs e) => new Order.FollowOrderPopUpWindow().ShowDialog();
        private void ShowCatalogButton_Click(object sender, RoutedEventArgs e) => new Cart.CatalogWindow(currentCart).ShowDialog();
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new Product.ProductListWindow().ShowDialog();
        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new Order.OrderListWindow().ShowDialog();
        private void btnSimulator_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().ShowDialog();


        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();
    }
}
