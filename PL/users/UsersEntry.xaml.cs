﻿using System;
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
using System.Data.SqlClient;


namespace PL.users
{
    /// <summary>
    /// Interaction logic for UsersEntry.xaml
    /// </summary>
    public partial class UsersEntry : Window
    {
        public UsersEntry()
        {
            InitializeComponent();
        }
        private void btnConfirmUser_Click(object sender, RoutedEventArgs e)
        {
            //check the given info
            new MainWindow().Show();
        }
    }
}