using BlApi;
using BO;
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

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
            //set default values:
            p.txtID.Text = "0";
            p.txtPrice.Text = "0";
            p.txtInStock.Text = "0";
            //p.cmbCategorySelector.Text = "None";

            p.btnAdd.Visibility = Visibility.Visible;
            p.btnUpdate.Visibility = Visibility.Hidden; 
            p.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
        }

        private void UpdateProductButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new Product.ProductWindow();
            var productList = bl.Product.GetListedProducts().ToList(); //get the current product list (which is in the same order as ProductListView)
            var productForList = productList[ProductListView.SelectedIndex]; //the product user clicked on
            //set default values:
            p.txtID.Text = productForList?.Id.ToString();
            p.txtID.IsEnabled = false;
            p.txtPrice.Text = productForList?.Price.ToString();
            //p.txtInStock.Text =;                            כמות מוצרים היא 0 או נאל ?????????????????????????????????
            p.txtName.Text = productForList?.Name;
            p.cmbCategorySelector.Text = productForList?.Category.ToString();
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
