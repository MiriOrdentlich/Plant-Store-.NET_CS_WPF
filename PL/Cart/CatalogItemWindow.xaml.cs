using System;
using System.Windows;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CatalogItemWindow.xaml
    /// </summary>
    public partial class CatalogItemWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.ProductItem? prodItemCurrent
        {
            get { return (BO.ProductItem?)GetValue(prodItemCurrentProperty); }
            set { SetValue(prodItemCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prodItemCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty prodItemCurrentProperty =
            DependencyProperty.Register("prodItemCurrent", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));

        public BO.Cart currentCart { set; get; } //currentCart will be initialized with user cart, will be used to add the current product item to cart

        public CatalogItemWindow(BO.Cart userCart, int idProductItem) //window gets the user cart by reference because we want all the changes to be applied on the same cart
        {
            InitializeComponent();
            currentCart = userCart; //currentCart points to userCart
            try
            {
                prodItemCurrent = bl?.Product.GetByIdC(idProductItem, currentCart);
            }
            catch (Exception exception)
            {
                prodItemCurrent = null;
                MessageBox.Show(exception.ToString());
                this.Close();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.AddItem(currentCart, prodItemCurrent!.Id);
                MessageBox.Show("Product item added to cart successfully");
                this.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        
    }
}
