using BlApi;
using System;
using System.Windows;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new BlImplementation.Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (CategorySelector.SelectedItem != null)
                ProductListView.ItemsSource = bl.Product.GetListedProducts(x => x?.Category.ToString() == CategorySelector.SelectedItem.ToString());
          
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new Product.ProductWindow();
            
            p.btnAdd.Visibility = Visibility.Visible;
            p.btnUpdate.Visibility = Visibility.Hidden; 
            p.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
        }

        private void UpdateProductButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new Product.ProductWindow();
         
            p.btnAdd.Visibility = Visibility.Hidden;
            p.btnUpdate.Visibility = Visibility.Visible;
            p.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            CategorySelector.SelectedItem = null;
        }
    }
}
