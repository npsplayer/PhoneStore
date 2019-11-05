﻿using PhoneStore.Model;
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

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        OracleDbContext db = null;
        public Register()
        {
            InitializeComponent();
            db = new OracleDbContext();
        }
        public void Registration()
        {
            db = new OracleDbContext();
            if (Validation.ValidateRegisterAndLogin(Username, ErrorUsername, IconUsername,
                                                    Password, ErrorPassword, IconPassword,
                                                    ConfirmPassword, ErrorConfirmPassword, IconConfirmPassword)) { 
                //User user = new User()
                //{
                //    Username = Username.Text,
                //    Password = Password.Password
                //};
                //db.Users.Add(user);

                //Address address = new Address();
                //db.Addresses.Add(address);

                //Customer customer = new Customer()
                //{
                //    UserID = user.UserID,
                //    AddressID = address.AddressID
                //};
                //db.Customers.Add(customer);
                //db.SaveChanges();


                
                MainWindow.Snackbar.IsActive = true;
                MainWindow.SnackbarMessage.Content = "Registration successful!";
                this.Close();
            }
            //else
            //{
            //    RegisterSnackBar.IsActive = true;
            //    SnackBarMessage.Content = "Username,password and confirm password are not entered correctly!\nCheck the correctness of the entered data!";
            //}
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Registration();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            RegisterSnackBar.IsActive = false;
        }
    }
}
