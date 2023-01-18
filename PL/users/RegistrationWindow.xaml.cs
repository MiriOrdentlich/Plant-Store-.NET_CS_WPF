using BO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;


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
            DependencyProperty.Register("user", typeof(BO.User), typeof(RegistrationWindow), new PropertyMetadata(null));

        public RegistrationWindow()
        {
            InitializeComponent();
            user = new BO.User()
            {
                Name = "",
                Address = "",
                Email = "",
                isManager = false,
                Password = ""
            };
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
                if (user?.Name == "")
                    throw new BlInvalidEntityException("Name", 1);
                if (user?.Address == "")
                    throw new BlInvalidEntityException("Address", 1);
                if (!checkEmail())
                    throw new BlInvalidEntityException("Email address", 1);
                if (user?.Password == "")
                    throw new BlInvalidEntityException("Password", 1);

                bl.User.Add(user);
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
