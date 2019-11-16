using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using PhoneStore.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        
        OracleDbContext db = null;
        int value = 1;
        public static ItemsControl Comment;
        public static Label Rating;
        public static RatingBar ProductRat;
        public ProductView()
        {
            
            int product = ShowCatalog.productid;
            InitializeComponent();
            db = new OracleDbContext();
            Comment = CommentView;
            Rating = TextRating;
            ProductRat = ProductRating;
            var Product = db.Products.Where(po => po.ProductID == product);
            ViewProduct.ItemsSource = Product.ToList();
            Header.ItemsSource = Product.ToList();
            var OptionMain = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 1 && productOption.ProductID == product
                             select new {  option.OptionID, option.OptionName , productOption.Value, productOption.Unit };
            ViewOptionMain.ItemsSource = OptionMain.ToList().OrderBy(order=>order.OptionID);

            var OptionProcessor = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 2 && productOption.ProductID == product
                             select new { option.OptionID, option.OptionName, productOption.Value, productOption.Unit };
            ViewProcessor.ItemsSource = OptionProcessor.ToList().OrderBy(order => order.OptionID);

            var OptionDesign = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 3 && productOption.ProductID == product
                                  select new { option.OptionID, option.OptionName, productOption.Value, productOption.Unit };
            ViewDesign.ItemsSource = OptionDesign.ToList().OrderBy(order => order.OptionID);

            var OptionSize = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 4 && productOption.ProductID == product
                                  select new { option.OptionID, option.OptionName, productOption.Value, productOption.Unit };
            ViewSize.ItemsSource = OptionSize.ToList().OrderBy(order => order.OptionID);

            var OptionCamera = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 5 && productOption.ProductID == product
                                  select new { option.OptionID, option.OptionName, productOption.Value, productOption.Unit };
            ViewCamera.ItemsSource = OptionCamera.ToList().OrderBy(order => order.OptionID);

            var OptionScreen = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 6 && productOption.ProductID == product
                                  select new { option.OptionID, option.OptionName, productOption.Value, productOption.Unit };
            ViewScreen.ItemsSource = OptionScreen.ToList().OrderBy(order => order.OptionID);
            try
            {
                var rating_out = new OracleParameter("rating_out", OracleDbType.Int32, ParameterDirection.Output);
                var sql = "BEGIN RATINGPRODUCT(" + product + ", :rating_out); END;";
                var rating = db.Database.ExecuteSqlCommand(sql, rating_out);
                ProductRating.Value = Convert.ToInt32(rating_out.Value.ToString());
                TextRating.Content = "(Rating: " + rating_out.Value.ToString() + ")";
            }
            catch { }
            var comment = from review in db.Reviews
                          join customer in db.Customers on review.CustomerID equals customer.CustomerID
                          join user in db.Users on customer.UserID equals user.UserID
                          where review.ProductID == product
                          select new { review.Description, user.Username, review.Rating, review.Date };
            CommentView.ItemsSource = comment.ToList().OrderBy(ob => ob.Date);
        }

        private void BackCatalog_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OneClick_Click(object sender, RoutedEventArgs e)
        {
            View.Basket basket = new View.Basket();
            var product = (Product)((Button)sender).Tag;
            //проверка на customerID
            if (Login.CustomerID != 0)
            {
                Model.OrderHistory orderHistory = new OrderHistory()
                {
                    ProductID = product.ProductID,
                    CustomerID = Login.CustomerID,
                    Status = "Paid",
                    Date = DateTime.Now,
                    Amount = Convert.ToInt32(CurrentAmount.Text),
                    Price = Convert.ToDouble(CurrentAmount.Text) * Convert.ToDouble(ShowCatalog.pricepoduct),
                    KeyFindProduct = 0
                };
                db.OrderHistories.Add(orderHistory);
                db.SaveChanges();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "You paid in one click!";
            }
            else if (Login.CustomerID == 0)
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "To buy goods in one click you need to register or log in to your account!";
            }
        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {
            //PROCEDURE
            View.Basket basket = new View.Basket();
            var product = (Product)((Button)sender).Tag;
            if (Login.CustomerID != 0)
            {
                var checkbasket = db.Baskets.Where(bs => bs.ProductID == product.ProductID && bs.CustomerID == Login.CustomerID);
                if (checkbasket.Count() != 0)
                {
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Product already added to cart!";
                }
                else
                {
                    Model.Basket basketModel = new Model.Basket()
                    {
                        CustomerID = Login.CustomerID,
                        ProductID = product.ProductID,
                        Amount = Convert.ToInt32(CurrentAmount.Text),
                        Price = Convert.ToDouble(CurrentAmount.Text)*Convert.ToDouble(ShowCatalog.pricepoduct)
                    };
                    db.Baskets.Add(basketModel);
                    db.SaveChanges();
                    var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
                    MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
                    Model.OrderHistory orderHistory = new OrderHistory()
                    {
                        CustomerID = Login.CustomerID,
                        ProductID = product.ProductID,
                        Date = DateTime.Now,
                        Status = "In basket",
                        KeyFindProduct = basketModel.BasketID,
                        Amount = Convert.ToInt32(CurrentAmount.Text),
                        Price = Convert.ToDouble(CurrentAmount.Text) * Convert.ToDouble(ShowCatalog.pricepoduct)
                    };
                    db.OrderHistories.Add(orderHistory);
                    db.SaveChanges();
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Add to cart!";
                }
            }
            else if (Login.CustomerID == 0)
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "To add to the basket you need to register or log in to your account!";
            }
        }

        private void MinusProduct_Click(object sender, RoutedEventArgs e)
        {
            if(value <= 1)
            {
                MinusProduct.IsEnabled = false;
            }
            else if (value > 0)
            {
                
                value--;
                CurrentAmount.Text = Convert.ToString(value);
            }
        }

        private void PlusProduct_Click(object sender, RoutedEventArgs e)
        {
            if (value > 0)
            {
                MinusProduct.IsEnabled = true;
                value++;
                CurrentAmount.Text = Convert.ToString(value);
            }
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }

        private void AddRewiew_Click(object sender, RoutedEventArgs e)
        {
            if (Login.CustomerID == 0)
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "To leave comments you need to register or log in to your account!";
            }
            else
            {
                View.AddRewiew addRewiew = new AddRewiew();
                addRewiew.ShowDialog();
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var comment = from review in db.Reviews
                          join customer in db.Customers on review.CustomerID equals customer.CustomerID
                          join user in db.Users on customer.UserID equals user.UserID
                          where review.ProductID == ShowCatalog.productid
                          select new { review.Description, user.Username, review.Rating, review.Date };
            CommentView.ItemsSource = comment.ToList().OrderByDescending(ob => ob.Date);
            PopupBoxHeader.Text = "New first";
        }

        private void Old_Click(object sender, RoutedEventArgs e)
        {
            var comment = from review in db.Reviews
                          join customer in db.Customers on review.CustomerID equals customer.CustomerID
                          join user in db.Users on customer.UserID equals user.UserID
                          where review.ProductID == ShowCatalog.productid
                          select new { review.Description, user.Username, review.Rating, review.Date };
            CommentView.ItemsSource = comment.ToList().OrderBy(ob => ob.Date);
            PopupBoxHeader.Text = "Old first";
        }
    }
}
