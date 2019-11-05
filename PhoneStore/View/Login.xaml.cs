using MaterialDesignThemes.Wpf;
using PhoneStore.Model;
using PhoneStore.UserControls;
using System;
using System.Collections.Generic;
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
        public Login()
        {
            InitializeComponent();
            db = new OracleDbContext();
            db.Users.Load();
        }
        public void LogIn()
        {
            db = new OracleDbContext();
            var checkuser = db.Users.Where(user => user.Username.Equals(Username.Text) && user.Password.Equals(Password.Password)).FirstOrDefault();
            if (Validation.ValidateRegisterAndLogin(Username, ErrorUsername,IconUsername, 
                                                    Password, ErrorPassword, IconPassword, 
                                                    null, null, null))
            {
                if (checkuser != null)
                {
                    UserID = checkuser.UserID;
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.ExitAccountBtn.IsEnabled = true;
                    MainWindow.SnackbarMessage.Content = "Welcome, " + checkuser.Username + "!";
                    this.Close();
                }
                if (checkuser == null)
                {

                    LoginSnackBar.IsActive = true;
                    SnackBarMessage.Content = "Username and password are not entered correctly!\nCheck the correctness of the entered data!";

                }
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
