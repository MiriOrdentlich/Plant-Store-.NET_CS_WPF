using BO;
using System.Windows;


namespace PL.Order
{
    /// <summary>
    /// Interaction logic for FollowOrderWindow.xaml
    /// </summary>
    public partial class FollowOrderWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
        private int OrderId;
        public BO.OrderTracking ordTrack
        {
            get { return (BO.OrderTracking)GetValue(ordTrackProperty); }
            set { SetValue(ordTrackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ordTrack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ordTrackProperty =
            DependencyProperty.Register("ordTrack", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));

        public FollowOrderWindow(int id)
        {
            InitializeComponent();
            OrderId = id;

            //trackingListView = bl.Order.TrackOrder(OrderId);
            trackingListView.ItemsSource = bl.Order.TrackOrder(OrderId).Tracking;
        }

        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
