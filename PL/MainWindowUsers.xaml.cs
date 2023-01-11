using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindowUsers.xaml
    /// </summary>
    public partial class MainWindowUsers : Window
    {
        public MainWindowUsers()
        {
            InitializeComponent();
        }
        private void btnUserEntry_Click(object sender, RoutedEventArgs e) => new Users.UserEntry().ShowDialog();
        private void btnNewUserRegister_Click(object sender, RoutedEventArgs e) => new Users.RegistrationWindow().ShowDialog();
        
    }
}
