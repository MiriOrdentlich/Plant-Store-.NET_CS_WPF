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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow(int idBOOrder = -1)
        {
            InitializeComponent();
            idTextBox.Text = idBOOrder.ToString();
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFollowOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
