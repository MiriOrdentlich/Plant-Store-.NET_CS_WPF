
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public OrderListWindow()        {
            InitializeComponent();
            orderForListDataGrid.ItemsSource = bl.Order.getOrdersList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            StatusSelector.SelectedItem = BO.Category.None;
        }

        private void StatusSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowOrderList();
            //if (CategorySelector.SelectedItem != null)
            //    OrderListView.ItemsSource = bl.Order.GetListedOrders(x => x?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void UpdateOrderButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (orderForListDataGrid.ItemsSource != null)
            {
                var p = (BO.OrderForList?)orderForListDataGrid.SelectedItem;
                int id = p?.Id ?? 0;
                new OrderWindow(id).Show();
                orderForListDataGrid.ItemsSource = bl?.Order.getOrdersList();

            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = BO.Category.None;
            ShowOrderList();
            //OrderListView.ItemsSource = bl.Order.GetListedOrders();
            //CategorySelector.SelectedItem = null;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowOrderList();
        }

        private void ShowOrderList()
        {
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            BO.OrderStatus? status = StatusSelector.SelectedItem as BO.OrderStatus?;
            if (status == BO.OrderStatus.Shipped)
                logicOrders = new(bl.Order.getOrdersList());
            else
                logicOrders = new(bl.Order.getOrdersList(/*x => x!.Category == category*/));
            orderForListDataGrid.ItemsSource = logicOrders;
            //if (StatusSelector.SelectedItem != null)
            //    OrderListView.ItemsSource = bl.Order.GetListedOrders(x => x?.Status.ToString() == StatusSelector.SelectedItem.ToString());
        }

    }
}



