using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Users
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.User? user
        {
            get { return (BO.User?)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(Window), new PropertyMetadata(null));

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private bool checkEmail()
        {
            string email = user?.Email ?? "";
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        private void btnConfirmUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check the given info:
                if (user?.Name is null)
                    throw new Exception("Invalid name");
                if (user?.Address is null)
                    throw new Exception("Invalid address");
                if (!checkEmail())
                    throw new Exception("Invalid email address");
                if (user?.Password is null)
                    throw new Exception("Invalid password");

                var tmp = new BO.User()
                {
                    Name = user?.Name,
                    Address = user?.Address,
                    Email = user?.Email,
                    isManager = false,
                    Password = user?.Password
                };
                bl.User.Add(tmp);
                //MessageBox.Show(ord.Id.ToString());
                MainWindow mw = new MainWindow(0); //send 0 to mainWindow because user can't register as manager
                mw.currentCart = new BO.Cart()
                {
                    CustomerAddress = user?.Address,
                    CustomerEmail = user?.Email,
                    CustomerName = user?.Name,
                    Items = new List<BO.OrderItem>(),
                    TotalPrice = 0
                };
                mw.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
