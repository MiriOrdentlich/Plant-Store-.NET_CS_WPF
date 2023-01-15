using System;
using System.Collections.Generic;
using System.Windows;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.Cart currentCart
        {
            get { return (BO.Cart)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(CheckoutWindow), new PropertyMetadata(null));

        public CheckoutWindow(BO.Cart userCart)
        {
            InitializeComponent();
            currentCart = userCart;
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order ord = bl.Cart.ConfirmCart(currentCart, currentCart?.CustomerName ?? "", currentCart?.CustomerEmail ?? "", currentCart?.CustomerAddress ?? "");
                MessageBox.Show("Your order has been confirmed \nOrder ID: "+ ord.Id.ToString());
                MainWindow mw = new MainWindow(0); //send 0 to mainWindow because manager can not enter checkoutWindow

                //mw.btnOrdersList.Visibility = Visibility.Hidden; //user isn't a manager
                //mw.btnProductsList.Visibility = Visibility.Hidden; //user isn't a manager
                //mw.currentCart = new BO.Cart()
                //{
                //    CustomerAddress = user?.Address,
                //    CustomerEmail = user?.Email,
                //    CustomerName = user?.Name,
                //    Items = new List<BO.OrderItem>(),
                //    TotalPrice = 0
                //}; 
                mw.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
