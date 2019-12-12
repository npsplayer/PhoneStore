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
            Brand.ItemsSource = db.Products.Local;
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

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            var product = (Product)((Button)sender).Tag;
            productid = product.ProductID;
            if (Login.CustomerID != 0)
            {
                if (db.ProductComparisons.LongCount() < 5)
                {
                    var checkcompare = db.ProductComparisons.Where(pc => pc.ProductID == productid && pc.CustomerID == Login.CustomerID);
                    if (checkcompare.Count() != 0)
                    {
                        MainWindow.Snackbar.IsActive = true;
                        MainWindow.SnackbarMessage.Content = "The product has already been added to the comparison!";
                    }
                    else
                    {
                        Model.ProductComparison productComparison = new ProductComparison()
                        {
                            ProductID = product.ProductID,
                            CustomerID = Login.CustomerID
                        };
                        db.ProductComparisons.Add(productComparison);
                        db.SaveChanges();
                    }
                }
                else
                {
                    MainWindow.Snackbar.IsActive = true;
                    MainWindow.SnackbarMessage.Content = "In comparison of goods there can be no more than 5 products!";
                }
            
            }
            else if (Login.CustomerID == 0)
            {
                MainWindow.Snackbar.IsActive = true;
                MainWindow.SnackbarMessage.Content = "To add to favorites you need to register or log in to your account!";
            }
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ListViewCatalog.ItemsSource = db.Products.Local;
            ToPrice.Text = String.Empty;
            FromPrice.Text = String.Empty;
            Brand.Text = String.Empty;
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            int toprice = 1000000;
            int fromprice = 0;
            if (ToPrice.Text != String.Empty)
            {
                toprice = Convert.ToInt32(ToPrice.Text);
            }
            if (FromPrice.Text != String.Empty)
            {
                fromprice = Convert.ToInt32(FromPrice.Text);
            }
            
            var select = from product in db.Products where product.Manufacturer == Brand.Text select product;
            var select1 = from product in db.Products where product.Price > fromprice select product;
            var select2 = from product in db.Products where product.Price < toprice select product;
            var select3 = from product in db.Products where product.Price < toprice && product.Price > fromprice select product;
            var select4 = from product in db.Products where product.Manufacturer == Brand.Text && product.Price > fromprice select product;
            var select5 = from product in db.Products where product.Manufacturer == Brand.Text && product.Price < toprice select product;
            var select6 = from product in db.Products where product.Manufacturer == Brand.Text && product.Price < toprice && product.Price > fromprice select product;

            if(Brand.Text == String.Empty && ToPrice.Text == String.Empty && FromPrice.Text == String.Empty)
            ListViewCatalog.ItemsSource = db.Products.Local;
            else if(Brand.Text != String.Empty && ToPrice.Text == String.Empty && FromPrice.Text == String.Empty)
            {
                ListViewCatalog.ItemsSource = select.ToList();
            }
            else if(Brand.Text == String.Empty && ToPrice.Text == String.Empty && FromPrice.Text != String.Empty)
            {
                ListViewCatalog.ItemsSource = select1.ToList();
            }
            else if (Brand.Text == String.Empty && ToPrice.Text != String.Empty && FromPrice.Text == String.Empty)
            {
                ListViewCatalog.ItemsSource = select2.ToList();
            }
            else if (Brand.Text == String.Empty && ToPrice.Text != String.Empty && FromPrice.Text != String.Empty)
            {
                ListViewCatalog.ItemsSource = select3.ToList();
            }
            else if (Brand.Text != String.Empty && ToPrice.Text == String.Empty && FromPrice.Text != String.Empty)
            {
                ListViewCatalog.ItemsSource = select4.ToList();
            }
            else if (Brand.Text != String.Empty && ToPrice.Text != String.Empty && FromPrice.Text == String.Empty)
            {
                ListViewCatalog.ItemsSource = select5.ToList();
            }
            else if (Brand.Text != String.Empty && ToPrice.Text != String.Empty && FromPrice.Text != String.Empty)
            {
                ListViewCatalog.ItemsSource = select6.ToList();
            }
        }

        
        private void New_Click(object sender, RoutedEventArgs e)
        {
            var newproduct = db.Products.OrderByDescending(ob => ob.ProductID);
            ListViewCatalog.ItemsSource = newproduct.ToList();
            PopupBoxHeader.Text = "New First";
        }

        private void Old_Click(object sender, RoutedEventArgs e)
        {
            var newproduct = db.Products.OrderBy(ob => ob.ProductID);
            ListViewCatalog.ItemsSource = newproduct.ToList();
            PopupBoxHeader.Text = "Old First";

        }
    }
}
