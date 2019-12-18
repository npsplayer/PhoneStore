using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
                                                    ConfirmPassword, ErrorConfirmPassword, IconConfirmPassword))
            {
                try
                {
                    var USERNAME = new OracleParameter("Username", OracleDbType.NVarchar2, Username.Text, ParameterDirection.Input);
                    var PASSWORD = new OracleParameter("Password", OracleDbType.NVarchar2, User.setHash(Password.Password), ParameterDirection.Input);
                    var sqluser = "BEGIN REGISTERUSER(:Username, :Password); END;";
                    var sqladmin = "BEGIN REGISTEADMIN(:Username, :Password); END;";
                    var check = db.Users.Where(u => u.Role == "Admin");
                    var a = check.LongCount();
                    if(check.LongCount() == 0)
                    {
                        db.Database.ExecuteSqlCommand(sqladmin, USERNAME, PASSWORD);
                        MainWindow.Snackbar.IsActive = true;
                        MainWindow.SnackbarMessage.Content = "Registration successful!";
                        this.Close();
                    }
                    else if(check.LongCount() >= 1)
                    {
                        db.Database.ExecuteSqlCommand(sqluser, USERNAME, PASSWORD);
                        MainWindow.Snackbar.IsActive = true;
                        MainWindow.SnackbarMessage.Content = "Registration successful!";
                        this.Close();
                    }
                    
                    
                }
                catch
                {
                    RegisterSnackBar.IsActive = true;
                    SnackBarMessage.Content = "Username,password and confirm password are not entered correctly!\nCheck the correctness of the entered data!";
                }


            }
            
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
