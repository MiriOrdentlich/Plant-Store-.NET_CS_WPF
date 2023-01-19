using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        private bool checkEmail()
        {
            string email = currentCart?.CustomerEmail ?? "";
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check the given info:
                if (currentCart?.CustomerName == "")
                    throw new BlInvalidEntityException("Name", 1);
                if (currentCart?.CustomerAddress == "")
                    throw new BlInvalidEntityException("Address", 1);
                if (!checkEmail())
                    throw new BlInvalidEntityException("Email address", 1);

                BO.Order ord = bl.Cart.ConfirmCart(currentCart!, currentCart?.CustomerName ?? "", currentCart?.CustomerEmail ?? "", currentCart?.CustomerAddress ?? "");
                MessageBox.Show("Your order has been confirmed \nOrder ID: " + ord.Id.ToString());
                MainWindow mw = new MainWindow(0); //send 0 to mainWindow because manager can not enter checkoutWindow
                mw.currentCart = new BO.Cart()
                {
                    CustomerName = currentCart?.CustomerName,
                    CustomerAddress = currentCart?.CustomerAddress,
                    CustomerEmail = currentCart?.CustomerEmail,
                    Items = new List<BO.OrderItem>(),
                    TotalPrice = 0
                };
                this.Close();
                mw.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();
    }
}
