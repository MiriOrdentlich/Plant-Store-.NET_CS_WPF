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

        ////will be used as flag to know which options to make abailable\unavailale for user according to their status
        ////isManager==0(==false) => user is client, isManager==1(==true) => user is manager 
        //public int isManager
        //{
        //    get { return (int)GetValue(isManagerProperty); }
        //    set { SetValue(isManagerProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for isManager.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty isManagerProperty =
        //    DependencyProperty.Register("isManager", typeof(int), typeof(Window), new PropertyMetadata(0));


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
                prodCurrent = (idBOProduct != -1) ? bl?.Product.GetByIdM(idBOProduct) : /*null*/new BO.Product() { };
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
                //if (int.TryParse(prodCurrent.Id, out int id) == false)
                //    throw new BO.BlInvalidEntityException("ID", 1);
                //if (BO.Category.TryParse(categoryComboBox.Text, out BO.Category category) == false)
                //    throw new BO.BlInvalidEntityException("category", 1);
                //if (double.TryParse(priceTextBox.Text, out double price) == false)
                //    throw new BO.BlInvalidEntityException("price", 1);
                //if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                //    throw new BO.BlInvalidEntityException("amount", 1);                
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
                //if (int.TryParse(idTextBox.Text, out int id) == false)
                //    throw new BO.BlInvalidEntityException("ID", 1);
                //if (double.TryParse(priceTextBox.Text, out double price) == false)
                //    throw new BO.BlInvalidEntityException("price", 1);
                //if (int.TryParse(inStockTextBox.Text, out int amount) == false)
                //    throw new BO.BlInvalidEntityException("amount", 1);
                bl.Product.UpdateProduct(prodCurrent!);
                MessageBox.Show("Product updated successfully");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
