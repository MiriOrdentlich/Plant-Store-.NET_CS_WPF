using System;
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
            currentCart = userCart;
        }

        /// <summary>
        /// refresh items grid to the current state of items in cart and update the total price
        /// </summary>
        private void refresh()
        {
            orderItemDataGrid.Items.Refresh();
            txtTotalPrice.Text = currentCart.TotalPrice.ToString();
        }

        /// <summary>
        /// for adding to cart one item from product that already in cart (the '+' button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderItemDataGrid.ItemsSource != null)
                {
                    var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                    int amnt = item?.Amount ?? -1;
                    currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, amnt + 1);
                }
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// for deleteing one item from product in cart (the '-' button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderItemDataGrid.ItemsSource != null)
                {
                    var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                    int amnt = item?.Amount ?? 0;
                    currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, amnt - 1);
                }
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// for deleting all items from a product in cart (the recycle bin button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                if (currentCart.Items != null)
                {
                    var item = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                    currentCart = bl.Cart.UpdateItemAmount(currentCart, item?.ProductID ?? -1, 0);
                }
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// delete all items in cart
        /// </summary>
        /// <param name="sender">from button 'empty cart'</param>
        /// <param name="e"></param>
        private void btnDeleteCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BO.OrderItem>? tmp = new();
                currentCart.Items = tmp;//release current item list and create a new empty item list
                orderItemDataGrid.ItemsSource = currentCart.Items; //update the binding
                currentCart.TotalPrice = 0; //update the new price
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// the confirm button is for user in case they to finish shopping 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new CheckoutWindow(currentCart).ShowDialog();
        }

        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();

        /// <summary>
        /// for returning to catalog to countinue shopping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new CatalogWindow(currentCart).ShowDialog();
        }
    }
}
