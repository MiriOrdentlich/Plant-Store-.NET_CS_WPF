using BO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        //IEnumerable<string> e = GetEnumDescriptions<Category>();

        public ObservableCollection<BO.ProductForList?> logicProducts
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(logicProductsProperty); }
            set { SetValue(logicProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for logicProducts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty logicProductsProperty =
            DependencyProperty.Register("logicProducts", typeof(ObservableCollection<BO.ProductForList?>), typeof(ProductListWindow), new PropertyMetadata(null));

        public ProductListWindow()
        {
            InitializeComponent();
            logicProducts = new(bl?.Product.GetListedProducts()!);
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategorySelector.SelectedItem = BO.Category.None;
        }

        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            ShowProductList();
            //if (CategorySelector.SelectedItem != null)
            //    ProductListView.ItemsSource = bl.Product.GetListedProducts(x => x?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new Product.ProductWindow();
            ////set default values:
            //p.idTextBox.Text = "0";
            //p.priceTextBox.Text = "0";
            //p.inStockTextBox.Text = "0";
            ////p.cmbCategorySelector.Text = "None";

            //p.btnAdd.Visibility = Visibility.Visible;
            //p.btnUpdate.Visibility = Visibility.Hidden;
            p.ShowDialog();
            productDataGrid.Items.Refresh();
        }

        private void UpdateProductButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (productDataGrid.ItemsSource != null)
            {
                var p = (BO.ProductForList?)productDataGrid.SelectedItem;
                int id = p?.Id ?? -1;
                //new ProductWindow(id).Show();
                ProductWindow productWindow = new Product.ProductWindow(id);
                productWindow.ShowDialog();
                productDataGrid.Items.Refresh();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CategorySelector.SelectedItem = BO.Category.None;
            ShowProductList();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowProductList();
        }

        private void ShowProductList()
        {
            try
            {

                BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
                if (category == BO.Category.None)
                    logicProducts = new(bl.Product.GetListedProducts());
                else
                    logicProducts = new(bl.Product.GetListedProducts(x => x!.Category == category));
                productDataGrid.Items.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            //this.Close();
            //new ProductListWindow().ShowDialog();
        }

        private void DeleteButton_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                var p = (BO.ProductForList?)productDataGrid.SelectedItem;
                var boProduct = bl.Product.GetByIdM(p?.Id ?? -1);
                bl.Product.DeleteProduct(p?.Id ?? -1);
                ShowProductList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        //private void DeleteButton_Click(object sender, RoutedEventArgs e)
        //{
        //    bl.Product.DeleteProduct(prodCurrent?.Id ?? -1);
        //    this.Close();
        //    new ProductListWindow().ShowDialog();
        //}

        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

