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
using PhoneStore.UserControls;

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

        public static TextBlock CountBasket;
        OracleDbContext db = null;
        int customer = Login.CustomerID;
        public MainWindow()
        {
            
            InitializeComponent();
            ShowCatalogUC = ShowCatalog;
            PersonalAccoutUC = PersonalAccout;

            Snackbar = SnackBar;
            SnackbarMessage = SnackBarMessage;
            ExitAccountBtn = ExitAccount;
            CountBasket = CountInBasket;
            

        }
       

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
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
            MainWindow.ShowCatalogUC.Visibility = Visibility.Hidden;
        }

        private void LogoName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.PersonalAccoutUC.Visibility = Visibility.Hidden;
            MainWindow.ShowCatalogUC.Visibility = Visibility.Visible;
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            Login.UserID = 0;
            Login.AddressID = 0;
            Login.CustomerID = 0;
            CountBasket.Text = "0";
            App.CurrentUser = null;
            if (PersonalInfo.SaveBut != null)
            {
                PersonalInfo.SaveBut.IsEnabled = false;
            }
            if(View.Basket.DeleteAllBtn != null)
            {
                View.Basket.DeleteAllBtn.IsEnabled = false;
            }
            if (View.Basket.PayBtn != null)
            {
                View.Basket.PayBtn.IsEnabled = false;
            }
           
            ExitAccount.IsEnabled = false;
            SnackBarMessage.Content = "You exit account!";
            SnackBar.IsActive = true;

        }

        private void Basket_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            View.Basket basket = new View.Basket();
            basket.ShowDialog();
        }
        public void SearchResults(string name)
        {
            db = new OracleDbContext();
            if (!string.IsNullOrEmpty(name))
            {
                var result = db.Products.Where(p => p.Name.Contains(name));
                SearchResult.ItemsSource = result.ToList();
            }
            if(name == String.Empty || name == "")
            {
                SearchResult.ItemsSource = null;
            }

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchResults(Search.Text);
        }

        private void Search_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupSearch.IsPopupOpen = true;
        }

        private void GridResult_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var product = (Model.Product)((Grid)sender).Tag;
            int? id = product.ProductID;
            ShowCatalog.productid = (int)id;
            ProductView productView = new ProductView();
            productView.ShowDialog();
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(239, 83, 80);
            Color secondaryColor = Color.FromRgb(239, 83, 80);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(171, 71, 188);
            Color secondaryColor = Color.FromRgb(171, 71, 188);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void DeepPurple_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(126, 87, 194);
            Color secondaryColor = Color.FromRgb(126, 87, 194);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Indigo_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(92, 107, 192);
            Color secondaryColor = Color.FromRgb(92, 107, 192);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(66, 165, 245);
            Color secondaryColor = Color.FromRgb(66, 165, 245);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Teal_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(38, 166, 154);
            Color secondaryColor = Color.FromRgb(38, 166, 154);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Color.FromRgb(102, 187, 106);
            Color secondaryColor = Color.FromRgb(102, 187, 106);
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            Color primaryColor = Colors.Black;
            Color secondaryColor = Colors.Black;
            IBaseTheme baseTheme = Theme.Light;
            ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
            paletteHelper.SetTheme(theme);
        }
    }
}
