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
    /// Логика взаимодействия для Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public static Button ExitAccountBtn;
        public static Button PersonalAccountBtn;
        public Header()
        {
            InitializeComponent();
            ExitAccountBtn = ExitAccount;
            PersonalAccountBtn = PersonalAccount;
            
        }
        
        

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LoginUC.Visibility = Visibility.Visible;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SignUpUC.Visibility = Visibility.Visible;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.win.Close();
        }

        private void PersonalAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PersonalAccoutUC.Visibility = Visibility.Visible;
            UserControls.PersonalAccount.PersonalInfoUC.Visibility = Visibility.Hidden;
            UserControls.PersonalAccount.PersonalAccountStackPanel.Visibility = Visibility.Visible;
            ShowCatalog.FilterUC.Visibility = Visibility.Hidden;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Hidden;


        }

        private void LogoName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.PersonalAccoutUC.Visibility = Visibility.Hidden;
            UserControls.PersonalAccount.PersonalInfoUC.Visibility = Visibility.Hidden;
            UserControls.PersonalAccount.PersonalAccountStackPanel.Visibility = Visibility.Visible;
            ShowCatalog.FilterUC.Visibility = Visibility.Visible;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Visible;
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
