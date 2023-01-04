using System.Windows;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        BO.Cart currentCart;

        public CheckoutWindow(BO.Cart userCart)
        {
            InitializeComponent();
            currentCart = userCart;
        }
    }
}
