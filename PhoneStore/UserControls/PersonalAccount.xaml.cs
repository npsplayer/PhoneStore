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
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : UserControl
    {
        
        public PersonalAccount()
        {
            InitializeComponent();
        }

        private void PersonalInfo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            View.PersonalInfo personalInfo = new View.PersonalInfo();
            personalInfo.ShowDialog();
        }


        private void Back_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.PersonalAccoutUC.Visibility = Visibility.Hidden;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Visible;
        }

        private void Basket_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            View.Basket basket = new View.Basket();
            basket.ShowDialog();
        }

        private void History_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            View.History history = new View.History();
            history.ShowDialog();
        }

        private void Compare_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Favorite_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            View.Favorite favorite = new View.Favorite();
            favorite.ShowDialog();
        }
    }
}
