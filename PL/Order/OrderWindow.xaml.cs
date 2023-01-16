using BO;
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
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.Order? orderCurrent
        {
            get { return (BO.Order?)GetValue(OrderCurrentProperty); }
            set { SetValue(OrderCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderCurrentProperty =
            DependencyProperty.Register("orderCurrent", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public OrderWindow(int idBOOrder = -1)
        {
            InitializeComponent();
            try
            {
                orderCurrent = (idBOOrder != -1) ? bl?.Order.GetOrderInfo(idBOOrder) : null;
                if (orderCurrent != null)
                    orderItemDataGrid.ItemsSource = orderCurrent?.Items;
            }
            catch (BO.BlMissingEntityException exception)
            {
                orderCurrent = null;
                MessageBox.Show(exception.ToString());
                this.Close();
            }
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderStatus? stat = orderCurrent!.Status;
                if (stat == BO.OrderStatus.Confirmed)
                    orderCurrent = bl?.Order.UpdateOrderShipping(orderCurrent?.Id ?? -1);
                else if (stat == BO.OrderStatus.Shipped)
                    orderCurrent = bl?.Order.UpdateOrderDelivery(orderCurrent?.Id ?? -1);
                MessageBox.Show("Status updated successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btnFollowOrder_Click(object sender, RoutedEventArgs e) //check if 
        {
            new FollowOrderWindow(orderCurrent?.Id ?? -1).ShowDialog();
        }

        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
