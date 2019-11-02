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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        OracleDbContext db = null;
        public Register()
        {
            InitializeComponent();
            db = new OracleDbContext();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            User user = new User()
            {
                Username = Username.Text,
                Password = Password.Password
            };
            db.Users.Add(user);
            db.SaveChanges();
            MainWindow.SignUpUC.Visibility = Visibility.Hidden;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SignUpUC.Visibility = Visibility.Hidden;
        }
    }
}
