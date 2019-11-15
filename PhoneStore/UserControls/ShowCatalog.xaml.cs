using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using PhoneStore.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для ShowCatalog.xaml
    /// </summary>
    public partial class ShowCatalog : UserControl
    {
        OracleDbContext db = null;
        public static int productid;
        public ShowCatalog()
        {
            InitializeComponent();
            db = new OracleDbContext();
            ShowPhone();
        }
        
        private void ShowPhone()
        {
            db = new OracleDbContext();
           
            db.Products.Load();
            db.ProductOptions.Load();
            ListViewCatalog.ItemsSource = db.Products.Local;
        }
        

        private void BuyProduct_Click(object sender, RoutedEventArgs e)
        {
           var product = (Product)((Button)sender).Tag;
            productid = product.ProductID;
            ProductView productView = new ProductView();
            productView.ShowDialog();

        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {
            View.Basket basket = new View.Basket(this);
            var product = (Product)((Button)sender).Tag;
            if (Login.CustomerID != 0)
            {
                var checkbasket = db.Baskets.Where(bs => bs.ProductID == product.ProductID && bs.CustomerID == Login.CustomerID);
                if (checkbasket != null)
                {
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "Product already added to cart!";
                }
                else 
                {
                    Model.Basket basketModel = new Model.Basket()
                    {
                        CustomerID = Login.CustomerID,
                        ProductID = product.ProductID,
                        Amount = 1
                    };
                    db.Baskets.Add(basketModel);
                    db.SaveChanges();
                }
            }
            else if(Login.CustomerID == 0)
            {
                MainWindow.Snackbar.IsActive = true;
                MainWindow.SnackbarMessage.Content = "In order to view personal data, you must register or login to your account!";
            }
        }
    }
}
