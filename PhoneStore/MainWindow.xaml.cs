using MaterialDesignThemes.Wpf;
using PhoneStore.Model;
using PhoneStore.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static UserControl ShowCatalogUC;
        public static UserControl FilterUC;
        public static UserControl PersonalAccoutUC;
        
        public static Button ExitAccountBtn;
        public static Button PersonalAccountBtn;

        public static Snackbar Snackbar;
        public static SnackbarMessage SnackbarMessage;


        public MainWindow()
        {
            InitializeComponent();
            ShowCatalogUC = ShowCatalog;
            PersonalAccoutUC = PersonalAccout;

            Snackbar = LoginSnackBar;
            SnackbarMessage = SnackBarMessage;
            ExitAccountBtn = ExitAccount;



        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            LoginSnackBar.IsActive = false;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            View.Login login = new View.Login();
            login.ShowDialog();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            View.Register register = new View.Register();
            register.ShowDialog();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Window.Close();
        }

        private void PersonalAccount_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccoutUC.Visibility = Visibility.Visible;
            UserControls.ShowCatalog.FilterUC.Visibility = Visibility.Hidden;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Hidden;
        }

        private void LogoName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.PersonalAccoutUC.Visibility = Visibility.Hidden;
            UserControls.ShowCatalog.FilterUC.Visibility = Visibility.Visible;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Visible;
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            Login.UserID = 0;
            App.CurrentUser = null;
            PersonalInfo.SaveBut.IsEnabled = false;
            ExitAccount.IsEnabled = false;
            SnackBarMessage.Content = "You exit account!";
            LoginSnackBar.IsActive = true;

        }
    }
}
