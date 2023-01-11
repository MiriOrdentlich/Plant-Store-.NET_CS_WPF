using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

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
                //#################### CHECK IF FIELDS ARE VALID #####################

                MainWindow mw = new MainWindow();
                if(user!.isManager)
                {
                    mw.btnCatalog.Visibility = Visibility.Hidden;
                }
                else //user isn't a manager
                {
                    mw.btnOrdersList.Visibility = Visibility.Hidden;
                    mw.btnProductsList.Visibility = Visibility.Hidden;
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
                new RegistrationWindow().ShowDialog();
                this.Close();
            }
        }

    }
}