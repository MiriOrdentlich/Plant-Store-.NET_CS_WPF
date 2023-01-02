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

        public BO.Category CategoryFilter { get; set; } = BO.Category.None;

        public OrderListWindow()
        {
            InitializeComponent();
            OrderListView.ItemsSource = bl.Order.getOrdersList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //CategoryFilter = BO.Category.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource OrderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("OrderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // OrderViewSource.Source = [generic data source]
        }
        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowOrderList();
            //if (CategorySelector.SelectedItem != null)
            //    OrderListView.ItemsSource = bl.Order.GetListedOrders(x => x?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void UpdateOrderButton_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            OrderWindow p = new Order.OrderWindow();
            var OrderList = bl.Order.getOrdersList().ToList(); //get the current Order list (which is in the same order as OrderListView)
            var OrderForList = OrderList[OrderListView.SelectedIndex]; //the Order user clicked on
                                                                         //set default values:
            p.idTextBox.Text = OrderForList?.Id.ToString();
            p.idTextBox.IsEnabled = false;
            p.totalPriceTextBox.Text = OrderForList?.TotalPrice.ToString();
            p.customerNameTextBox.Text = OrderForList?.CustomerName;
            p.customerAddressTextBox.Text = OrderForList?.CustomerName;
            p.emailNameTextBox.Text = OrderForList?.CustomerName;
            p.categoryComboBox.Text = OrderForList?.Category.ToString();
            p.btnAdd.Visibility = Visibility.Hidden;
            p.btnUpdate.Visibility = Visibility.Visible;
            p.ShowDialog();
            //OrderListView.ItemsSource = bl.Order.GetListedOrders();

            //int id = ((BO.OrderForList?)(sender as OrderListView)?.DataContext)?.Id
            //    ?? throw new NullReferenceException("null event sender");
            //new OrderWindow(id).Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
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
            //BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
            if (CategoryFilter == BO.Category.None)
                //OrderListView.ItemsSource = bl.Order.GetListedOrders();
                logicOrders = new(bl.Order.GetListedOrders());
            else
                //OrderListView.ItemsSource = bl.Order.GetListedOrders(BO.Filter);        
                logicOrders = new(bl.Order.GetListedOrders(x => x!.Category == CategoryFilter));
        }
    }
}


}


