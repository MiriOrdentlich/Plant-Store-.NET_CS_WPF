using System;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// event handler for a new category filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProductList();
        }

        /// <summary>
        /// event handler in case a click on the add button. this will open
        /// a window to enter the new product details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new Product.ProductWindow();
            p.ShowDialog();
            productDataGrid.Items.Refresh();
        }

        /// <summary>
        /// event handler in case of 2 clicks on a product details in grid. this will open
        /// the product details for update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProductButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (productDataGrid.SelectedItem as BO.ProductForList != null)//make sure it's not a random click
            {
                var p = (BO.ProductForList?)productDataGrid.SelectedItem;
                int id = p?.Id ?? -1;
                ProductWindow productWindow = new Product.ProductWindow(id);
                productWindow.ShowDialog();
                productDataGrid.Items.Refresh();
            }
        }

        /// <summary>
        /// to cancel the category selector (which returns to its default- NONE)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CategorySelector.SelectedItem = BO.Category.None;
            ShowProductList();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowProductList();
        }

        /// <summary>
        /// get 8 most popular products (by grouping)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopularsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logicProducts = new(bl.Product.GetListedPopularItems());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();
    }
}

