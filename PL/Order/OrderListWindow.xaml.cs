using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public ObservableCollection<BO.OrderForList?> logicOrders
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(logicOrdersProperty); }
            set { SetValue(logicOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for logicOrders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty logicOrdersProperty =
            DependencyProperty.Register("logicOrders", typeof(ObservableCollection<BO.OrderForList?>), typeof(OrderListWindow), new PropertyMetadata(null));

        public OrderListWindow()
        {
            InitializeComponent();
            OrderListView.ItemsSource = bl.Order.getOrdersList();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource OrderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("OrderViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // OrderViewSource.Source = [generic data source]
        //}

        private void UpdateOrderButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            OrderWindow p = new Order.OrderWindow();
            var OrderList = bl.Order.getOrdersList().ToList(); //get the current Order list (which is in the same order as OrderListView)
            var OrderForList = OrderList[OrderListView.SelectedIndex]; //the Order user clicked on
            //set values:
            p.idTextBox.Text = OrderForList?.Id.ToString();
            p.idTextBox.IsEnabled = false;
            p.totalPriceTextBox.Text = OrderForList?.TotalPrice.ToString();
            p.customerNameTextBox.Text = OrderForList?.CustomerName;
            p.customerAddressTextBox.Text = OrderForList?.CustomerName;
            p.customerEmailTextBox.Text = OrderForList?.CustomerName;
            p.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowOrderList();
        }

        private void ShowOrderList()
        {
            logicOrders = new(bl.Order.getOrdersList());
        }
    }
}



