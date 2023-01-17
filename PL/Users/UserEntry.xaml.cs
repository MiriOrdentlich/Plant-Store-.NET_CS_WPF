using System;
using System.Collections.Generic;
using System.Windows;

namespace PL.Users
{
    /// <summary>
    /// Interaction logic for UserEntry.xaml
    /// </summary>
    public partial class UserEntry : Window
    {
        private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;

        public BO.User user
        {
            get { return (BO.User)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(Window), new PropertyMetadata(null));


        public UserEntry()
        {
            InitializeComponent();
        }

        private void btnConfirmUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check the given info:
                if (customerNameTextBox.Text =="")
                    throw new Exception("Invalid name");
                if (PasswordTextBox.Text =="")
                    throw new Exception("Invalid password");

                //var tmp = bl.User.Get(user.Name!, user.Password!) ;
                user = bl.User.Get(customerNameTextBox.Text, PasswordTextBox.Text)!;
                //user.isManager = tmp!.isManager;
                //user.Address = tmp.Address;
                //user.Email = tmp.Email;
                int flag = user.isManager ? 1 : 0;

                MainWindow mw = new MainWindow(flag);

                if(user!.isManager == false) //user is a client
                {

                    mw.currentCart = new BO.Cart()
                    {
                        CustomerAddress = user.Address,
                        CustomerEmail = user.Email,
                        CustomerName = user.Name,
                        Items = new List<BO.OrderItem>(),
                        TotalPrice = 0
                    };
                }
                mw.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                //new RegistrationWindow().ShowDialog();
                //this.Close();
            }
        }

    }
}