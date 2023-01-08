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
        private void Registered_Click(object sender, RoutedEventArgs e) => new UsersEntry().Show();

        private void Register_Click(object sender, RoutedEventArgs e) => new Registration().Show();
    }
}
