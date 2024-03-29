﻿using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using PhoneStore.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        OracleDbContext db = null;
        public static int UserID = 0;
        public static int AddressID = 0;
        public static int CustomerID = 0;
        public Login()
        {
            InitializeComponent();
        }
        public void LogIn()
        {
            try
            {

                db = new OracleDbContext();
                
                var USERNAME = new OracleParameter("Username", OracleDbType.NVarchar2, Username.Text, ParameterDirection.Input);
                var PASSWORD = new OracleParameter("Password", OracleDbType.NVarchar2, User.setHash(Password.Password), ParameterDirection.Input);
                var ROLE_OUT = new OracleParameter("Role_OUT", OracleDbType.NVarchar2, ParameterDirection.Output); ROLE_OUT.Size = 50;
                var USERID_OUT = new OracleParameter("UserID_OUT", OracleDbType.Int32, ParameterDirection.Output);
                var ADDRESSID_OUT = new OracleParameter("AddressID_OUT", OracleDbType.Decimal, ParameterDirection.Output);
                var CUSTOMERID_OUT = new OracleParameter("CustomerID_OUT", OracleDbType.Decimal, ParameterDirection.Output);
                var sql = "BEGIN LOGIN(:Username, :Password, :UserID_OUT, :AddressID_OUT, :CustomerID_OUT, :Role_OUT); END;";           
                if (Validation.ValidateRegisterAndLogin(Username, ErrorUsername, IconUsername,
                                                        Password, ErrorPassword, IconPassword,
                                                        null, null, null))
                {
                    
                    var checkUser = db.Database.ExecuteSqlCommand(sql, USERNAME, PASSWORD, USERID_OUT, ADDRESSID_OUT, CUSTOMERID_OUT, ROLE_OUT);
                    if(ROLE_OUT.Value.ToString() == "Admin")
                    {
                        MainWindow.AdminStackPanel.Visibility = Visibility.Visible;
                    }
                    if(ROLE_OUT.Value.ToString() == "User")
                    {
                        MainWindow.AdminStackPanel.Visibility = Visibility.Hidden;
                    }
                        UserID = Convert.ToInt32(USERID_OUT.Value.ToString());
                        AddressID = Convert.ToInt32(ADDRESSID_OUT.Value.ToString());
                        CustomerID = Convert.ToInt32(CUSTOMERID_OUT.Value.ToString());
                        MainWindow.Snackbar.IsActive = true;
                        MainWindow.ExitAccountBtn.IsEnabled = true;
                        if (View.Basket.DeleteAllBtn != null)
                        {
                        View.Basket.DeleteAllBtn.IsEnabled = false;
                        }
                        if (View.Basket.PayBtn != null)
                        {
                        View.Basket.PayBtn.IsEnabled = false;
                        }
                        var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
                        MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
                        MainWindow.SnackbarMessage.Content = "Welcome, " + USERNAME.Value + "!";
                        this.Close();
                    
                }
            } catch 
            {
                LoginSnackBar.IsActive = true;
                SnackBarMessage.Content = "Username and password are not entered correctly!\nCheck the correctness of the entered data!";
            }            
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            LoginSnackBar.IsActive = false;
        }

        private void LoginButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                this.Close();
            }
            else if(e.Key == Key.Enter)
            {
                LoginSnackBar.IsActive = false;
            }
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginSnackBar.IsActive = false;
        }
    }
}
