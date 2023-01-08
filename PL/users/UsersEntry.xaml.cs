//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Data.SqlClient;


//namespace PL.users
//{
//    /// <summary>
//    /// Interaction logic for UsersEntry.xaml
//    /// </summary>
//    public partial class UsersEntry : Window
//    {
//        public UsersEntry()
//        {
//            InitializeComponent();
//        }
//        private void btnConfirmUser_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                BO.Order ord = bl.Users.ConfirmCart(currentCart, customerNameTextBox.Text, customerEmailTextBox.Text, customerAddressTextBox.Text);
//                MessageBox.Show(ord.Id.ToString());
//            }
//            catch (Exception exception)
//            {
//                MessageBox.Show(exception.ToString());
//            }
//            new MainWindow().Show();
//        }

//    }
//}