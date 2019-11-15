using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        ShowCatalog catalogView;
        OracleDbContext db = null; 
        
        public ProductView()
        {
            int product = ShowCatalog.productid;
            InitializeComponent();
            db = new OracleDbContext();
            db.Products.Load();
            db.Options.Load();
            db.OptionTypes.Load();
            db.ProductOptions.Load();

            var Product = db.Products.Where(po => po.ProductID == product);
            ViewProduct.ItemsSource = Product.ToList();
            Header.ItemsSource = Product.ToList();
            var OptionMain = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 1 && productOption.ProductID == product
                             select new { option.OptionName , productOption.Value, productOption.Unit };
            ViewOptionMain.ItemsSource = OptionMain.ToList();

            var OptionProcessor = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 2 && productOption.ProductID == product
                             select new { option.OptionName, productOption.Value, productOption.Unit };
            ViewProcessor.ItemsSource = OptionProcessor.ToList();

            var OptionDesign = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 3 && productOption.ProductID == product
                                  select new { option.OptionName, productOption.Value, productOption.Unit };
            ViewDesign.ItemsSource = OptionDesign.ToList();

            var OptionSize = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 4 && productOption.ProductID == product
                                  select new { option.OptionName, productOption.Value, productOption.Unit };
            ViewSize.ItemsSource = OptionSize.ToList();

            var OptionCamera = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 5 && productOption.ProductID == product
                                  select new { option.OptionName, productOption.Value, productOption.Unit };
            ViewCamera.ItemsSource = OptionCamera.ToList();

            var OptionScreen = from productOption in db.ProductOptions
                                  join option in db.Options on productOption.OptionID equals option.OptionID
                                  join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                                  where optiontype.OptionTypeID == 6 && productOption.ProductID == product
                                  select new { option.OptionName, productOption.Value, productOption.Unit };
            ViewScreen.ItemsSource = OptionScreen.ToList();
            ////ListView.ItemsSource = select.ToList();
            //ListView.ItemsSource = db.OptionTypes.Local;
        }

        private void BackCatalog_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OneClick_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {

            View.Basket basket = new View.Basket();
            var product = (Product)((Button)sender).Tag;
            if (product != null)
            {
                Model.Basket basketModel = new Model.Basket()
                {
                    CustomerID = Login.CustomerID,
                    ProductID = product.ProductID,
                    Amount = 1
                };
                db.Baskets.Add(basketModel);
                db.SaveChanges();
            } else if (product == null)
            {

            }
        }
    }
}
