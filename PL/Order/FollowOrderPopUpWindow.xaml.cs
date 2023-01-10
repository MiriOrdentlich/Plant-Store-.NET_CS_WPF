using PL.users;
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
        public FollowOrderPopUpWindow()
        {
            InitializeComponent();
        }

        private void FollowOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idTextBox.Text != "")
                {
                    if (int.TryParse(idTextBox.Text, out int id) == false)
                        throw new BO.BlInvalidEntityException("ID", 1);
                    new FollowOrderWindow(id).ShowDialog();
                }
                else
                    MessageBox.Show("Please Enter Id");
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
