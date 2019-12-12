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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        OracleDbContext db = null;
        int value = 0;
        public static int productID;
        public int customer = Login.CustomerID;
        public static Button PayBtn;
        public static Button DeleteAllBtn;
        public Basket()
        {
            InitializeComponent();
            db = new OracleDbContext();
            ShowBasket();
            PayBtn = PayButton;
            DeleteAllBtn = DeleteAllButton;
        }
        public void ShowBasket()
        {
            db.Products.Load();
            db.Baskets.Load();
            var select = db.Baskets.Where(bask => bask.CustomerID == customer);
            ListViewBasket.ItemsSource = select.ToList();
            try
            {
                var totalprice_out = new OracleParameter("totalprice_out", OracleDbType.Int32, ParameterDirection.Output);
                var sql = "BEGIN TOTALPRICEBASKET(" + customer + ", :totalprice_out); END;";
                var total = db.Database.ExecuteSqlCommand(sql, totalprice_out);
                
              
                TotalPrice.Text = totalprice_out.Value.ToString();
                if (TotalPrice.Text == "null")
                {
                    TotalPrice.Text = "0";
                }
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Opppps....Try again?";
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       

        
        private void Delete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var basket = (Model.Basket)((TextBlock)sender).Tag;
                int? basketid = basket.BasketID;
                var sql = "BEGIN BASKETDELETEONE(" + basketid + "," + customer + "); END;";
                var update = db.Database.ExecuteSqlCommand(sql);
                ShowBasket();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "You delete product from cart!";
                var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
                MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "There were problems uninstalling. Try later!";
            }
        }


        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            //PROCEDURE
            var deleteall = db.Baskets.Where(bs => bs.CustomerID == customer).ToList();
            foreach(var row in deleteall)
            {
                db.Baskets.Remove(row);
                var update = db.OrderHistories.Where(oh => oh.CustomerID == customer && oh.KeyFindProduct == row.BasketID).FirstOrDefault();
                update.Status = "Delete from basket";
            }
            
            db.SaveChanges();
            ShowBasket();
            var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
            MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            var update = db.OrderHistories.Where(oh => oh.CustomerID == customer).ToList();
            foreach(var row in update)
            {
                row.Status = "Paid";
            }
            db.SaveChanges();
            var deleteall = db.Baskets.Where(bs => bs.CustomerID == customer).ToList();
            foreach (var row in deleteall)
            {
                db.Baskets.Remove(row);
            }
            db.SaveChanges();
            ShowBasket();
            var select = db.Baskets.Where(bask => bask.CustomerID == Login.CustomerID);
            MainWindow.CountBasket.Text = Convert.ToString(select.LongCount());
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var product = (Model.Basket)((Image)sender).Tag;
            int? id = product.ProductID;
            ShowCatalog.productid = (int)id;
            ProductView productView = new ProductView();
            productView.ShowDialog();
        }
    }
}
