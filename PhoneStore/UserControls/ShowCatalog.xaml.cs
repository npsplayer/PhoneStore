using PhoneStore.Model;
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
        public static UserControl FilterUC;
        public ShowCatalog()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<OracleDbContext>());
            InitializeComponent();
            db = new OracleDbContext();
            ShowPhone();
            FilterUC = Filter;


        }
        private void IntiPhone()
        {
            db = new OracleDbContext();
            Phone phone1 = new Phone() { Camera = "Front: 8MP; Rear: 12.2MP", OS = "Android", Screen = "6'3'", Processors = "Qualcomm Snapdragon™ 855", Price = "600" };
            Phone phone2 = new Phone() { Camera = "Front: 8MP;", OS = "iOS", Screen = "6'3'", Processors = "Qualcomm Snapdragon™ 865", Price = "700" };
            Phone phone3 = new Phone() { Camera = "Front: 8MP; Rear: 12.2MP", OS = "Android", Screen = "6'3'", Processors = "Qualcomm Snapdragon™ 875", Price = "800" };
            Phone phone4 = new Phone() { Camera = "Front: 8MP;", OS = "iOS", Screen = "6'3'", Processors = "Qualcomm Snapdragon™ 885", Price = "900" };
            Phone phone5 = new Phone() { Camera = "Front: 8MP; Rear: 12.2MP", OS = "Android", Screen = "6'3'", Processors = "Qualcomm Snapdragon™ 895", Price = "1000" };
            db.Phones.Add(phone1);
            db.Phones.Add(phone2);
            db.Phones.Add(phone3);
            db.Phones.Add(phone4);
            db.Phones.Add(phone5);
            db.SaveChanges();
        }
        private void ShowPhone()
        {
            db = new OracleDbContext();
            if (db.Phones.Count() == 0)
            {
                IntiPhone();
            }
            db.Phones.Load();
            ListViewCatalog.ItemsSource = db.Phones.Local;
        }

        private void BuyProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
