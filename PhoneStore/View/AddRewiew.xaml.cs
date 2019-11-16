using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using PhoneStore.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AddRewiew.xaml
    /// </summary>
    public partial class AddRewiew : Window
    {
        OracleDbContext db = null;
        public AddRewiew()
        {
            InitializeComponent();
            db = new OracleDbContext();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Review review = new Model.Review()
            {
                CustomerID = Login.CustomerID,
                ProductID = ShowCatalog.productid,
                Description = Comment.Text,
                Rating = Rating.Value,
                Date = DateTime.Now
            };
            db.Reviews.Add(review);
            db.SaveChanges();
            var comment = from rev in db.Reviews
                          join customer in db.Customers on rev.CustomerID equals customer.CustomerID
                          join user in db.Users on customer.UserID equals user.UserID
                          where rev.ProductID == ShowCatalog.productid
                          select new { rev.Description, user.Username, rev.Rating };
            ProductView.Comment.ItemsSource = comment.ToList();
            try
            {
                var rating_out = new OracleParameter("rating_out", OracleDbType.Int32, ParameterDirection.Output);
                var sql = "BEGIN RATINGPRODUCT(" + ShowCatalog.productid + ", :rating_out); END;";
                var rating = db.Database.ExecuteSqlCommand(sql, rating_out);
                ProductView.ProductRat.Value = Convert.ToInt32(rating_out.Value.ToString());
                ProductView.Rating.Content = "(Rating: " + rating_out.Value.ToString() + ")";
            }
            catch { }
            this.Close();
        }
    }
}
