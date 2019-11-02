using PhoneStore.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneStore.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        OracleDbContext db = null;
        public static int UserID = 0;
        public Login()
        {
            InitializeComponent();
            db = new OracleDbContext();
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            var checkuser = db.Users.Where(user => user.Username.Equals(Username.Text)).FirstOrDefault();
            if (checkuser != null)
            {
                Header.ExitAccountBtn.IsEnabled = true;
                Header.PersonalAccountBtn.IsEnabled = true;
                MainWindow.LoginUC.Visibility = Visibility.Hidden;
                MainWindow.HeaderUC.Visibility = Visibility.Visible;
                UserID = checkuser.UserID;
               

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LoginUC.Visibility = Visibility.Hidden;
        }
    }
}
