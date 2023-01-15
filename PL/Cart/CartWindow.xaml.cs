using System.Collections.Generic;
using System.Windows;


namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.Cart currentCart
        {
            get { return (BO.Cart)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(CartWindow), new PropertyMetadata(null));

        public CartWindow(BO.Cart userCart)
        {
            InitializeComponent();
            //orderItemDataGrid.ItemsSource = userCart.Items;
            currentCart = userCart;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                int amnt = item?.Amount ?? -1;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, amnt + 1);
            }
            this.Close();
            new CartWindow(currentCart).ShowDialog();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                int amnt = item?.Amount ?? 0;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, amnt - 1);
            }
            this.Close();
            new CartWindow(currentCart).ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) //event to delete the whole order item; all the items of a product in the cart
        {
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, 0);
            }
            this.Close();
            new CartWindow(currentCart).ShowDialog();

        }

        private void btnDeleteCart_Click(object sender, RoutedEventArgs e)
        {
            currentCart.Items = new List<BO.OrderItem>(); //release current item list and create a new empty item list
            currentCart.TotalPrice = 0;
            this.Close();
            new CartWindow(currentCart).ShowDialog();
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            new CheckoutWindow(currentCart).ShowDialog();
            this.Close();
        }
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow(currentCart).ShowDialog();
            this.Close();
        }
    }
}
