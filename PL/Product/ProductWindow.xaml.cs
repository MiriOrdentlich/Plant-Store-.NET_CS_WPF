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
    public partial class ProductWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
        public ProductWindow()
        {
            InitializeComponent();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtID.Text, out int id) == false)
                    throw new BO.BlInvalidEntityException("ID", 1);
                if (BO.Category.TryParse(cmbCategorySelector.Text, out BO.Category category) == false)
                    throw new BO.BlInvalidEntityException("category", 1);
                if (double.TryParse(txtPrice.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                if (int.TryParse(txtInStock.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("amount", 1);                
                bl.Product.AddProduct(id, txtName.Text, category, price, amount);
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
                if (int.TryParse(txtID.Text, out int id) == false)
                    throw new BO.BlInvalidEntityException("ID", 1);
                if (double.TryParse(txtPrice.Text, out double price) == false)
                    throw new BO.BlInvalidEntityException("price", 1);
                if (int.TryParse(txtInStock.Text, out int amount) == false)
                    throw new BO.BlInvalidEntityException("amount", 1);
                BO.Product p = new BO.Product();
                p.Id=id;
                p.Name=txtName.Text;
                p.InStock=amount;
                p.Price=price;
                p.Category = (BO.Category)cmbCategorySelector.SelectedIndex;
                bl.Product.UpdateProduct(p);
                MessageBox.Show("Product updated successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
