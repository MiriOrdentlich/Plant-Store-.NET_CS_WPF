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

        public BO.Product? prodCurrent;
        //public ProductWindow()
        //{
        //    InitializeComponent();
        //    cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //}
        public ProductWindow(int idBOProduct = -1)
        {

            InitializeComponent();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //COMBINE ADD AND UPDATE?????????????
            txtID.Text = idBOProduct.ToString();

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
            //this.DataContext = prodCurrent;         
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
                //prodCurrent = new BO.Product();
                //prodCurrent.Id = id;
                //prodCurrent.Name = txtName.Text;
                //prodCurrent.InStock = amount;
                //prodCurrent.Price = price;
                //prodCurrent.Category = (BO.Category)cmbCategorySelector.SelectedIndex;
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
