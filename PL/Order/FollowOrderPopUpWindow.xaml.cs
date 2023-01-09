using PL.users;
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
            if (idTextBox.Text != "") 
            new FollowOrderWindow(Int32.Parse(idTextBox.Text)).Show();
            else
                MessageBox.Show("Please Enter Id");
        }

        private void btnBye_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
