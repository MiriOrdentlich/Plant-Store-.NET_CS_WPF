using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for FollowOrderPopUpWindow.xaml
    /// </summary>
    public partial class FollowOrderPopUpWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public FollowOrderPopUpWindow()
        {
            InitializeComponent();
        }

        private void FollowOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(idTextBox.Text, out int id) == false)
                    throw new BO.BlInvalidEntityException("ID", 1);
                var tmp = bl.Order.GetOrderInfo(id);
                new FollowOrderWindow(id).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
