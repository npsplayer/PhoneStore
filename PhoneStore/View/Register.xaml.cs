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
                //try
                //{
                    var USERNAME = new OracleParameter("Username", OracleDbType.NVarchar2, Username.Text, ParameterDirection.Input);
                    var PASSWORD = new OracleParameter("Password", OracleDbType.NVarchar2, Password.Password, ParameterDirection.Input);
                    var CITY = new OracleParameter("City", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var STREET = new OracleParameter("Street", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var HOUSENUMBER = new OracleParameter("HouseNumber", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var ROOM = new OracleParameter("Room", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var FIRSTNAME = new OracleParameter("Firstname", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var SECONDNAME = new OracleParameter("Secondname", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var PATRONYMIC = new OracleParameter("Patronymic", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var DATEOFBIRTH = new OracleParameter("DateOfBirth", OracleDbType.Date, DateTime.Now, ParameterDirection.Input);
                    var EMAIL = new OracleParameter("Email", OracleDbType.NVarchar2, null, ParameterDirection.Input);
                    var PHONENUMBER = new OracleParameter("PhoneNumber", OracleDbType.NVarchar2, null, ParameterDirection.Input);

                    var sql = "BEGIN REGISTER(:Username, :Password); END;";
                    db.Database.ExecuteSqlCommand(sql, USERNAME, PASSWORD);
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "Registration successful!";
                    this.Close();
                //}
                //catch
                //{
                //    RegisterSnackBar.IsActive = true;
                //    SnackBarMessage.Content = "Username,password and confirm password are not entered correctly!\nCheck the correctness of the entered data!";
                //}


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
