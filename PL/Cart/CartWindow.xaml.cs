using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        public CartWindow(BO.Cart userCart)
        {
            InitializeComponent();
            orderItemDataGrid.ItemsSource = userCart.Items;
            currentCart = userCart;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                int amnt = item?.Amount ?? 0;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? 0, amnt + 1);
                txtTotalPrice.Text = currentCart.TotalPrice.ToString();
            }
            orderItemDataGrid.Items.Refresh();

        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                int amnt = item?.Amount ?? 0;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? 0, amnt - 1);
                txtTotalPrice.Text=currentCart.TotalPrice.ToString();
            }
            orderItemDataGrid.Items.Refresh();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) //event to delete the whole order item; all the items of a product in the cart
        {
            if (orderItemDataGrid.ItemsSource != null)
            {
                var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? 0, 0);
                txtTotalPrice.Text = currentCart.TotalPrice.ToString();
            }
            orderItemDataGrid.Items.Refresh();

        }

        private void btnDeleteCart_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem?> tmp= new();            
            currentCart.Items = tmp; //release current item list and create a new empty item list
            currentCart.TotalPrice =0;
            orderItemDataGrid.ItemsSource = currentCart!.Items;
            txtTotalPrice.Text= currentCart.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            new CheckoutWindow(currentCart).Show();
            this.Close();
        }
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
