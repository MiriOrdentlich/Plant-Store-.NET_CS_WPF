using System;
using System.Collections.ObjectModel;
using System.Windows;

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
            orderForListDataGrid.ItemsSource = bl.Order.getOrdersList();
        }

        private void UpdateOrderButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (orderForListDataGrid.SelectedItem as BO.OrderForList != null)
            {
                var p = (BO.OrderForList?)orderForListDataGrid.SelectedItem;
                int id = p?.Id ?? -1;
                new OrderWindow(id).Show();
                orderForListDataGrid.ItemsSource = bl?.Order.getOrdersList();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowOrderList();
        }

        private void ShowOrderList()
        {
            logicOrders = new(bl.Order.getOrdersList());
            orderForListDataGrid.ItemsSource = logicOrders;
        }

        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();
    }
}



