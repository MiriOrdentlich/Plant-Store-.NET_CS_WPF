using System.Collections.Generic;
using System.Windows;


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
