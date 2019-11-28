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
    /// Логика взаимодействия для ProductCoparisonView.xaml
    /// </summary>
    public partial class ProductCoparisonView : Window
    {
        OracleDbContext db = null;
        public ProductCoparisonView()
        {
            InitializeComponent();
            Show();
        }
        public void Show()
        {
            db = new OracleDbContext();
            db.ProductComparisons.Load();
            var View = db.ProductComparisons.Where(p => p.CustomerID == Login.CustomerID).OrderBy(o=> o.ProductComparisonID);
            ViewProduct.ItemsSource = View.ToList();
            int product = ShowCatalog.productid;
            var OptionMain = db.Options.Where(o => o.OptionTypeID == 1).OrderBy(o=> o.OptionID);
            MainViewOption.ItemsSource = OptionMain.ToList().OrderBy(order => order.OptionID);
            
            var count = db.ProductComparisons.Where(p => p.CustomerID == Login.CustomerID).OrderBy(o=>o.ProductComparisonID).Select(s=>s.ProductID);
            var arr = count.ToArray();
            int? p0 = 0;
            if (count.LongCount() >= 1)
            {
                p0 = arr[0];
            }
            var MainZ = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 1 && productOption.ProductID == p0
                             select new { productOption.Value, productOption.Unit };
            MainZero.ItemsSource = MainZ.ToList();

            int? p1 = 0;
            if (count.LongCount() >= 2)
            {
                p1 = arr[1];
            }
            var MainO = from productOption in db.ProductOptions
                       join option in db.Options on productOption.OptionID equals option.OptionID
                       join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                       where optiontype.OptionTypeID == 1 && productOption.ProductID == p1
                       select new { productOption.Value, productOption.Unit };
            MainOne.ItemsSource = MainO.ToList();

            int? p2 = 0;
            if (count.LongCount() >= 3)
            {
                p2 = arr[2];
            }
            var MainT = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 1 && productOption.ProductID == p2
                        select new { productOption.Value, productOption.Unit };
            MainTwo.ItemsSource = MainT.ToList();

            int? p3 = 0;
            if (count.LongCount() >= 4)
            {
                p3 = arr[3];
            }
            var MainTh = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 1 && productOption.ProductID == p3
                        select new { productOption.Value, productOption.Unit };
            MainThree.ItemsSource = MainTh.ToList();

            int? p4 = 0;
            if (count.LongCount() >= 5)
            {
                p0 = arr[4];
            }

            var MainF = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 1 && productOption.ProductID == p4
                        select new { productOption.Value, productOption.Unit };
            MainFour.ItemsSource = MainF.ToList();

            var OptionProc = db.Options.Where(o => o.OptionTypeID == 2).OrderBy(o => o.OptionID);
            ProcessorViewOption.ItemsSource = OptionProc.ToList();

            var ProcessorZ = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 2 && productOption.ProductID == p0
                        select new { productOption.Value, productOption.Unit };
            ProcessorZero.ItemsSource = ProcessorZ.ToList();

            var ProcessorO = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 2 && productOption.ProductID == p1
                        select new { productOption.Value, productOption.Unit };
            ProcessorOne.ItemsSource = ProcessorO.ToList();

            var ProcessorT = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 2 && productOption.ProductID == p2
                        select new { productOption.Value, productOption.Unit };
            ProcessorTwo.ItemsSource = ProcessorT.ToList();

            var ProcessorTh = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 2 && productOption.ProductID == p3
                        select new { productOption.Value, productOption.Unit };
            ProcessorThree.ItemsSource = ProcessorTh.ToList();

            var ProcessorF = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 2 && productOption.ProductID == p4
                        select new { productOption.Value, productOption.Unit };
            ProcessorFour.ItemsSource = ProcessorF.ToList();

            var OptionDesign = db.Options.Where(o => o.OptionTypeID == 3).OrderBy(o => o.OptionID);
            DesignViewOption.ItemsSource = OptionProc.ToList();

            var DesignZ = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 3 && productOption.ProductID == p0
                             select new { productOption.Value, productOption.Unit };
            DesignZero.ItemsSource = DesignZ.ToList();

            var DesignO = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 3 && productOption.ProductID == p1
                             select new { productOption.Value, productOption.Unit };
            DesignOne.ItemsSource = DesignO.ToList();

            var DesignT = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 3 && productOption.ProductID == p2
                             select new { productOption.Value, productOption.Unit };
            DesignTwo.ItemsSource = DesignT.ToList();

            var DesignTh = from productOption in db.ProductOptions
                              join option in db.Options on productOption.OptionID equals option.OptionID
                              join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                              where optiontype.OptionTypeID == 3 && productOption.ProductID == p3
                              select new { productOption.Value, productOption.Unit };
            DesignThree.ItemsSource = DesignTh.ToList();

            var DesignF = from productOption in db.ProductOptions
                             join option in db.Options on productOption.OptionID equals option.OptionID
                             join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                             where optiontype.OptionTypeID == 3 && productOption.ProductID == p4
                             select new { productOption.Value, productOption.Unit };
            DesignFour.ItemsSource = DesignF.ToList();

            var OptionSize = db.Options.Where(o => o.OptionTypeID == 4).OrderBy(o => o.OptionID);
            SizeViewOption.ItemsSource = OptionSize.ToList();

            var SizeZ = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 4 && productOption.ProductID == p0
                          select new { productOption.Value, productOption.Unit };
            SizeZero.ItemsSource = SizeZ.ToList();

            var SizeO = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 4 && productOption.ProductID == p1
                          select new { productOption.Value, productOption.Unit };
            SizeOne.ItemsSource = SizeO.ToList();

            var SizeT = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 4 && productOption.ProductID == p2
                          select new { productOption.Value, productOption.Unit };
            SizeTwo.ItemsSource = SizeT.ToList();

            var SizeTh = from productOption in db.ProductOptions
                           join option in db.Options on productOption.OptionID equals option.OptionID
                           join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                           where optiontype.OptionTypeID == 4 && productOption.ProductID == p3
                           select new { productOption.Value, productOption.Unit };
            SizeThree.ItemsSource = SizeTh.ToList();

            var SizeF = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 4 && productOption.ProductID == p4
                          select new { productOption.Value, productOption.Unit };
            SizeFour.ItemsSource = SizeF.ToList();

            var OptionCamera = db.Options.Where(o => o.OptionTypeID == 5).OrderBy(o => o.OptionID);
            CameraViewOption.ItemsSource = OptionCamera.ToList();

            var CameraZ = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 5 && productOption.ProductID == p0
                        select new { productOption.Value, productOption.Unit };
            CameraZero.ItemsSource = CameraZ.ToList();

            var CameraO = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 5 && productOption.ProductID == p1
                        select new { productOption.Value, productOption.Unit };
            CameraOne.ItemsSource = CameraO.ToList();

            var CameraT = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 5 && productOption.ProductID == p2
                        select new { productOption.Value, productOption.Unit };
            CameraTwo.ItemsSource = CameraT.ToList();

            var CameraTh = from productOption in db.ProductOptions
                         join option in db.Options on productOption.OptionID equals option.OptionID
                         join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                         where optiontype.OptionTypeID == 5 && productOption.ProductID == p3
                         select new { productOption.Value, productOption.Unit };
            CameraThree.ItemsSource = CameraTh.ToList();

            var CameraF = from productOption in db.ProductOptions
                        join option in db.Options on productOption.OptionID equals option.OptionID
                        join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                        where optiontype.OptionTypeID == 5 && productOption.ProductID == p4
                        select new { productOption.Value, productOption.Unit };
            CameraFour.ItemsSource = CameraF.ToList();

            var OptionScreen = db.Options.Where(o => o.OptionTypeID == 6).OrderBy(o => o.OptionID);
            ScreenViewOption.ItemsSource = OptionScreen.ToList();

            var ScreenZ = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 6 && productOption.ProductID == p0
                          select new { productOption.Value, productOption.Unit };
            ScreenZero.ItemsSource = ScreenZ.ToList();

            var ScreenO = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 6 && productOption.ProductID == p1
                          select new { productOption.Value, productOption.Unit };
            ScreenOne.ItemsSource = ScreenO.ToList();

            var ScreenT = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 6 && productOption.ProductID == p2
                          select new { productOption.Value, productOption.Unit };
            ScreenTwo.ItemsSource = ScreenT.ToList();

            var ScreenTh = from productOption in db.ProductOptions
                           join option in db.Options on productOption.OptionID equals option.OptionID
                           join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                           where optiontype.OptionTypeID == 6 && productOption.ProductID == p3
                           select new { productOption.Value, productOption.Unit };
            ScreenThree.ItemsSource = ScreenTh.ToList();

            var ScreenF = from productOption in db.ProductOptions
                          join option in db.Options on productOption.OptionID equals option.OptionID
                          join optiontype in db.OptionTypes on option.OptionTypeID equals optiontype.OptionTypeID
                          where optiontype.OptionTypeID == 6 && productOption.ProductID == p4
                          select new { productOption.Value, productOption.Unit };
            ScreenFour.ItemsSource = ScreenF.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            var product = (ProductComparison)((TextBlock)sender).Tag;
            var delete = db.ProductComparisons.Where(p => p.ProductID == product.ProductID && p.CustomerID == Login.CustomerID).FirstOrDefault();
            db.ProductComparisons.Remove(delete);
            db.SaveChanges();
            Show();
            SnackBar.IsActive = true;
            SnackBarMessage.Content = "You delete product from compare!";
            
            

        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }

    }
}
