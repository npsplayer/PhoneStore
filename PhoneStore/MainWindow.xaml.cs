using PhoneStore.Model;
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
        public static Window win;
        public static UserControl LoginUC;
        public static UserControl ShowCatalogUC;
        public static UserControl FilterUC;
        public static UserControl HeaderUC;
        public static UserControl PersonalAccoutUC;
        public static UserControl SignUpUC;

        
        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            win = Window;
            LoginUC = Login;
            ShowCatalogUC = ShowCatalog;
            PersonalAccoutUC = PersonalAccout;
            HeaderUC = Header;
            SignUpUC = SignUp;


        }
        public MainWindow(UserControls.Header header)
        {
            
           
            
        }
    }
}
