using PhoneStore.Model;
using PhoneStore.UserControls;
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
using System.Windows.Shapes;

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для Favorite.xaml
    /// </summary>
    public partial class Favorite : Window
    {
        OracleDbContext db = null;
        public Favorite()
        {
            InitializeComponent();
            db = new OracleDbContext();
            ShowFavorite();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void ShowFavorite()
        {
            db = new OracleDbContext();
            var select = from favorite in db.Favorites
                         join customer in db.Customers on favorite.CustomerID equals customer.CustomerID
                         join product in db.Products on favorite.ProductID equals product.ProductID
                         select new { favorite, product };
            var selectnew = db.Favorites.Where(f => f.CustomerID == Login.CustomerID);
            ListViewFavorite.ItemsSource = selectnew.ToList();
        }
        private void Delete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var favorite = (Model.Favorite)((TextBlock)sender).Tag;
            int? favoriteid = favorite.FavoriteID;
            var delete = db.Favorites.Where(bs => bs.FavoriteID == favoriteid && bs.CustomerID == Login.CustomerID).FirstOrDefault();
            db.Favorites.Remove(delete);
            db.SaveChanges();
            ShowFavorite();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "You delete product from cart!";
            
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var product = (Model.Favorite)((Image)sender).Tag;
            int? id = product.ProductID;
            ShowCatalog.productid = (int)id;
            ProductView productView = new ProductView();
            productView.ShowDialog();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }
    }
}
