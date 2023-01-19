using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.Cart currentCart
        {
            get { return (BO.Cart)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(CatalogWindow), new PropertyMetadata(null));


        public ObservableCollection<BO.ProductItem?> listedProductItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(listedProductItemsProperty); }
            set { SetValue(listedProductItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listedProductItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listedProductItemsProperty =
            DependencyProperty.Register("listedProductItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
        
        public CatalogWindow(BO.Cart cart)
        {
            InitializeComponent();
            currentCart = cart;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new CartWindow(currentCart).Show();
        }

        private void productItemDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (catalog.SelectedItem as BO.ProductItem != null)
            {
                var p = (BO.ProductItem?)catalog.SelectedItem;
                int idProductItem = p?.Id ?? -1;
                new CatalogItemWindow(currentCart, idProductItem).Show();
                this.Show();
            }
        }

        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowProductItemsList();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CategorySelector.SelectedItem = BO.Category.None;
            ShowProductItemsList();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CategorySelector.SelectedItem = BO.Category.None;
        }

        private void ShowProductItemsList()
        {
            try
            {
                BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
                if (category == BO.Category.None)
                    listedProductItems = new(bl.Product.GetListedProductItems(currentCart));
                else
                    listedProductItems = new(bl.Product.GetListedProductItems(currentCart, x => x!.Category == category));
                catalog.ItemsSource = listedProductItems;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
