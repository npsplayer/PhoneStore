using Microsoft.Win32;
using PhoneStore.Model;
using PhoneStore.View;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CustomerPhoto.xaml
    /// </summary>
    public partial class CustomerPhoto : UserControl
    {
        OracleDbContext db = null;
        
        public CustomerPhoto()
        {
            InitializeComponent();
            ShowPhoto();
            
        }
        public void ShowPhoto()
        {
            db = new OracleDbContext();
            var select1 = db.Customers.Where(c => c.CustomerID == Login.CustomerID).FirstOrDefault();
            var select = db.Customers.Where(c => c.CustomerID == Login.CustomerID);

            if (select1.Photo == null)
            {


            }
            else
            {
                DefaultPhoto.Visibility = Visibility.Collapsed;
                PhotoControl.ItemsSource = select.ToList();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            USPhoto.Visibility = Visibility.Hidden;
            //PersonalInfo.PhotoV.Visibility = Visibility.Collapsed;
            
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            string pathFile = openFileDialog.FileName;
            FileStream fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imagecode = br.ReadBytes((Int32)fs.Length);
            var update = db.Customers.Where(c => c.CustomerID == Login.CustomerID).FirstOrDefault();
            update.Photo = imagecode;
            db.SaveChanges();
            ShowPhoto();
        }
    }
}
