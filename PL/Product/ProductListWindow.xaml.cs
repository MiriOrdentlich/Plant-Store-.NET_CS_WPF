using BO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;


        public ObservableCollection<BO.ProductForList?> logicProducts
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(logicProductsProperty); }
            set { SetValue(logicProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for logicProducts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty logicProductsProperty =
            DependencyProperty.Register("logicProducts", typeof(ObservableCollection<BO.ProductForList?>), typeof(ProductListWindow), new PropertyMetadata(null));

        public BO.Category CategoryFilter { get; set; } = BO.Category.None;

        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //CategoryFilter = BO.Category.None;
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
            //set default values:
            p.txtID.Text = "0";
            p.txtPrice.Text = "0";
            p.txtInStock.Text = "0";
            //p.cmbCategorySelector.Text = "None";

            p.btnAdd.Visibility = Visibility.Visible;
            p.btnUpdate.Visibility = Visibility.Hidden; 
            p.ShowDialog();
            //ProductListView.ItemsSource = bl.Product.GetListedProducts();
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
            p.txtName.Text = productForList?.Name;
            p.cmbCategorySelector.Text = productForList?.Category.ToString();
            p.btnAdd.Visibility = Visibility.Hidden;
            p.btnUpdate.Visibility = Visibility.Visible;
            p.ShowDialog();
            //ProductListView.ItemsSource = bl.Product.GetListedProducts();
            
            //int id = ((BO.ProductForList?)(sender as ProductListView)?.DataContext)?.Id
            //    ?? throw new NullReferenceException("null event sender");
            //new ProductWindow(id).Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProductList();
            //ProductListView.ItemsSource = bl.Product.GetListedProducts();
            //CategorySelector.SelectedItem = null;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowProductList();
        }

        private void ShowProductList()
        {
            //BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
            if (CategoryFilter == BO.Category.None)
                //ProductListView.ItemsSource = bl.Product.GetListedProducts();
                logicProducts = new(bl.Product.GetListedProducts());
            else
                //ProductListView.ItemsSource = bl.Product.GetListedProducts(BO.Filter);        
                logicProducts = new(bl.Product.GetListedProducts(x => x!.Category == CategoryFilter));
        }
    }
}

