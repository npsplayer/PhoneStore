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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            var checkuser = db.Users.Where(user => user.Username.Equals(Username.Text)).FirstOrDefault();
            if (checkuser != null)
            {
                UserID = checkuser.UserID;
                MainWindow.Snackbar.IsActive = true;
                MainWindow.ExitAccountBtn.IsEnabled = true;
                MainWindow.SnackbarMessage.Content = "Welcome, " + checkuser.Username;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
