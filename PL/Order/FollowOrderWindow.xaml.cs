﻿using BO;
using System.Windows;


namespace PL.Order
{
    /// <summary>
    /// Interaction logic for FollowOrderWindow.xaml
    /// </summary>
    public partial class FollowOrderWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

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
            ordTrack = bl.Order.TrackOrder(id);
        }

        /// <summary>
        /// close current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBye_click(object sender, RoutedEventArgs e) => this.Close();
    }
}
