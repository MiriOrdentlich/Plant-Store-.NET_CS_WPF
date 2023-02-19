using System;
using System.Windows;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow  : Window 
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.Product? prodCurrent
        {
            get { return (BO.Product?)GetValue(prodCurrentProperty); }
            set { SetValue(prodCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prodCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty prodCurrentProperty =
            DependencyProperty.Register("prodCurrent", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

        public ProductWindow(int idBOProduct = -1)
        {

            InitializeComponent();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            try
            {
                prodCurrent = (idBOProduct != -1) ?
                    bl?.Product.GetByIdM(idBOProduct) :
                    new BO.Product()
                    {
                        Id = 0,
                        Name = "",
                        Price = 0,
                        Category = BO.Category.None,
                        InStock = 0
                    };
            }
            catch (Exception exception)
            {
                prodCurrent = null;
                MessageBox.Show(exception.ToString());
                this.Close();
                new ProductListWindow().ShowDialog();
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //input check:
             
                if(prodCurrent?.Category == BO.Category.None)
                    throw new BO.BlInvalidEntityException("Category", 1);
                //if (BO.Category.TryParse(categoryComboBox.Text, out BO.Category category) == false)
                //    throw new BO.BlInvalidEntityException("Category", 1);
                if (prodCurrent?.Name == "")
                    throw new BO.BlInvalidEntityException("Name", 1);
                if (double.TryParse(priceTextBox.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("Price", 1);
                if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("Amount", 1);
                bl.Product.AddProduct(prodCurrent?.Id ?? -1 , prodCurrent?.Name ?? "", prodCurrent!.Category, prodCurrent.Price, prodCurrent.InStock);
                MessageBox.Show("Product added successfully");
                this.Close();            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //input check:
                if (prodCurrent?.Category == BO.Category.None)
                    throw new BO.BlInvalidEntityException("Category", 1);
                if (prodCurrent?.Name == "")
                    throw new BO.BlInvalidEntityException("Name", 1);
                if (double.TryParse(priceTextBox.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("Price", 1);
                if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("Amount", 1);
                bl.Product.UpdateProduct(prodCurrent!);
                MessageBox.Show("Product updated successfully");
                this.Close();
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
