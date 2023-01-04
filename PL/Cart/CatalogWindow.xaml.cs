using System;
using System.Collections;
using System.Collections.Generic;
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

        //public static BO.Cart currentCart = new BO.Cart() {
        //    CustomerAddress = "",
        //    CustomerEmail = "",
        //    CustomerName = "",
        //    Items = new List <BO.OrderItem> ,
        //    TotalPrice = 0 };

        public static BO.Cart currentCart = new BO.Cart()
        {
            CustomerAddress = "",
            CustomerEmail = "",
            CustomerName = "",
            Items = new List<BO.OrderItem>(),
            TotalPrice = 0
        };

        public ObservableCollection<BO.ProductItem?> listedProductItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(listedProductItemsProperty); }
            set { SetValue(listedProductItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for listedProductItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty listedProductItemsProperty =
            DependencyProperty.Register("listedProductItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
        
        public CatalogWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(currentCart).Show();
            this.Close();
        }

        private void productItemDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (productItemDataGrid.ItemsSource != null)
            {
                var p = (BO.ProductItem?)productItemDataGrid.SelectedItem;
                int idProductItem = p?.Id ?? 0;
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
            this.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CategorySelector.SelectedItem = BO.Category.None;
            //ShowProductItemsList();
            //productItemDataGrid.ItemsSource = bl.Product.GetListedProductItems(currentCart);
        }

        private void ShowProductItemsList()
        {
            BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
            if (category == BO.Category.None)
                listedProductItems = new(bl.Product.GetListedProductItems(currentCart));
            else
                listedProductItems = new(bl.Product.GetListedProductItems(currentCart, x => x!.Category == category));
            productItemDataGrid.ItemsSource = listedProductItems;    
        }
    }
}
