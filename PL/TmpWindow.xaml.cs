using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for TmpWindow.xaml
    /// </summary>
    public partial class TmpWindow : Window
    {
        public TmpWindow()
        {
            InitializeComponent();
        }
        private void ShowcartButton_Click(object sender, RoutedEventArgs e) => new Cart.CatalogWindow(
            new BO.Cart() 
        { 
                TotalPrice = 0,
                Items= new List<BO.OrderItem>(),
                CustomerName = "naama",
                CustomerAddress = "PT",
                CustomerEmail="naama@gmail.com"
        }).ShowDialog();
    }
}
