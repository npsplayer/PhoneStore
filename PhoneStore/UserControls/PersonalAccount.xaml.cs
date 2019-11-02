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
        public static UserControl PersonalInfoUC;
        public static StackPanel PersonalAccountStackPanel;
        public PersonalAccount()
        {
            InitializeComponent();
            PersonalInfoUC = PersonalInfoView;
            PersonalAccountStackPanel = PersonalAccountSP;
        }

        private void PersonalInfo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PersonalInfoView.Visibility = Visibility.Visible;
            PersonalAccountSP.Visibility = Visibility.Hidden;
        }
    }
}
