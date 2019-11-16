using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using PhoneStore.UserControls;
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
using System.Windows.Shapes;

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        OracleDbContext db = null;
        public int customer = Login.CustomerID;
        public History()
        {
            InitializeComponent();
            db = new OracleDbContext();
            db.Products.Load();
            db.OrderHistories.Load();
            ShowHistory();
        }
        public void ShowHistory()
        {
            
            var select = db.OrderHistories.Where(oh => oh.CustomerID == customer  && oh.Status == "Paid").OrderBy(oh => oh.Date);
            ListViewHistory.ItemsSource = select.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }

        private void SortDateAsc_Click(object sender, RoutedEventArgs e)
        {
            var select = db.OrderHistories.Where(oh => oh.CustomerID == customer && oh.Status == "Paid").OrderBy(oh => oh.Date);
            ListViewHistory.ItemsSource = select.ToList();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "Sorted by date ascending!";
        }

        private void SortDateDesc_Click(object sender, RoutedEventArgs e)
        {
            var select = db.OrderHistories.Where(oh => oh.CustomerID == customer && oh.Status == "Paid").OrderByDescending(oh => oh.Date);
            ListViewHistory.ItemsSource = select.ToList();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "Sorted by date in descending order!";
        }

        private void SortPriceAsc_Click(object sender, RoutedEventArgs e)
        {
            var select = db.OrderHistories.Where(oh => oh.CustomerID == customer && oh.Status == "Paid").OrderBy(oh => oh.Price);
            ListViewHistory.ItemsSource = select.ToList();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "Sorted by price ascending!";
        }

        private void SortPriceDesc_Click(object sender, RoutedEventArgs e)
        {
            var select = db.OrderHistories.Where(oh => oh.CustomerID == customer && oh.Status == "Paid").OrderByDescending(oh => oh.Price);
            ListViewHistory.ItemsSource = select.ToList();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "Sorted by price in descending order!";
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var product = (Model.OrderHistory)((Image)sender).Tag;
            int? id = product.ProductID;
            ShowCatalog.productid = (int)id;
            ProductView productView = new ProductView();
            productView.ShowDialog();
        }
    }
}
