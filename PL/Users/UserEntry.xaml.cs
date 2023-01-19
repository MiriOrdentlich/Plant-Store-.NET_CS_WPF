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
            DependencyProperty.Register("user", typeof(BO.User), typeof(UserEntry), new PropertyMetadata(null));


        public UserEntry()
        {
            InitializeComponent();
            user = new BO.User()
            {
                //set default values:
                Name = "",
                Address = "",
                Email = "",
                isManager = false,
                Password = ""
            };
        }

        /// <summary>
        /// if user already in data: open to the user the proper options according to their status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check the given info:
                if (user?.Name == "")
                    throw new BO.BlInvalidEntityException("Name", 1);
                if (user?.Password == "")
                    throw new BO.BlInvalidEntityException("Password", 1);

                user = bl.User.Get(user?.Name!, user?.Password!)!;
                
                int flag = user.isManager ? 1 : 0;
                MainWindow mw = new MainWindow(flag);
                if (user!.isManager == false) //user is a client
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
            }
        }

    }
}