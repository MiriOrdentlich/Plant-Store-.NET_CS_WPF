using BlApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

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
                prodCurrent = (idBOProduct != -1) ? bl?.Product.GetByIdM(idBOProduct) : null;
            }
            catch (Exception exception)
            {
                prodCurrent = null;
                MessageBox.Show(exception.ToString());
                this.Close();
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            idTextBox.IsReadOnly= false;
            try
            {
                if (int.TryParse(idTextBox.Text, out int id) == false)
                    throw new BO.BlInvalidEntityException("ID", 1);
                if (BO.Category.TryParse(categoryComboBox.Text, out BO.Category category) == false)
                    throw new BO.BlInvalidEntityException("category", 1);
                if (double.TryParse(priceTextBox.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("amount", 1);                
                bl.Product.AddProduct(id, nameTextBox.Text, category, price, amount);
                MessageBox.Show("Product added successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(idTextBox.Text, out int id) == false)
                    throw new BO.BlInvalidEntityException("ID", 1);
                if (double.TryParse(priceTextBox.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("amount", 1);
                bl.Product.UpdateProduct(prodCurrent!);
                MessageBox.Show("Product updated successfully");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
