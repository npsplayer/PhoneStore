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
        public static double pricepoduct;
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
            pricepoduct = product.Price;
            ProductView productView = new ProductView();
            productView.ShowDialog();

        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {
            var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
            View.Basket basket = new View.Basket();
            var product = (Product)((Button)sender).Tag;
            if (Login.CustomerID != 0)
            {
                var checkbasket = db.Baskets.Where(bs => bs.ProductID == product.ProductID && bs.CustomerID == Login.CustomerID);
                if (checkbasket.Count() != 0)
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
                        Amount = 1,
                        Price = product.Price
                    };
                    db.Baskets.Add(basketModel);
                    
                    db.SaveChanges();
                    MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
                    Model.OrderHistory orderHistory = new OrderHistory()
                    {
                        CustomerID = Login.CustomerID,
                        ProductID = product.ProductID,
                        Date = DateTime.Now,
                        Status = "In basket",
                        KeyFindProduct = basketModel.BasketID,
                        Amount = 1,
                        Price = product.Price
                    };
                    db.OrderHistories.Add(orderHistory);
                    db.SaveChanges();
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "Add to cart!";
                }
            }
            else if(Login.CustomerID == 0)
            {
                MainWindow.Snackbar.IsActive = true;
                MainWindow.SnackbarMessage.Content = "To add to the basket you need to register or log in to your account!";
            }
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            var product = (Product)((Button)sender).Tag;
            productid = product.ProductID;
            if (Login.CustomerID != 0)
            {
                var checkbasket = db.Favorites.Where(bs => bs.ProductID == product.ProductID && bs.CustomerID == Login.CustomerID);
                if (checkbasket.Count() != 0)
                {
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "The product is already in your favorites!";
                }
                else
                {
                    Model.Favorite favorite = new Model.Favorite()
                    {
                        CustomerID = Login.CustomerID,
                        ProductID = product.ProductID,
                    };
                    db.Favorites.Add(favorite);
                    db.SaveChanges();
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "Add to favorite!";
                }
            }
            else if (Login.CustomerID == 0)
            {
                MainWindow.Snackbar.IsActive = true;
                MainWindow.SnackbarMessage.Content = "To add to favorites you need to register or log in to your account!";
            }
        }
    }
}
