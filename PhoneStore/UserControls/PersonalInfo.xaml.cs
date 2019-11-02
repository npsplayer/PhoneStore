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
    /// Логика взаимодействия для PersonalInfo.xaml
    /// </summary>
    public partial class PersonalInfo : UserControl
    {
        OracleDbContext db = null;
        public PersonalInfo()
        {
            ShowPersonalInfo();
            InitializeComponent();
            db = new OracleDbContext();
        }

        public void ShowPersonalInfo()
        {
            db = new OracleDbContext();
            db.Customers.Load();
            db.Addresses.Load();
            db.Users.Load();
            var select1 = db.Customers.Where(cus => cus.UserID == Login.UserID).FirstOrDefault();
            if (select1 != null)
            {
                FirstName.Text = select1.FirstName;
                SecondName.Text = select1.SecondName;
                Patronymic.Text = select1.Patronymic;
                DateOfBirth.Text = Convert.ToString(select1.DateOfBirth);
                Email.Text = select1.Email;
                PhoneNumber.Text = select1.PhoneNumber;
                Street.Text = select1.Address.Street;
                HomeNumber.Text = select1.Address.HouseNumber;
                Entrance.Text = select1.Address.Entrance;
            }
            
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            Address address = new Address()
            {
                Street = Street.Text,
                HouseNumber = HomeNumber.Text,
                Entrance = Entrance.Text
            };
            db.Addresses.Add(address);
            Customer customer = new Customer()
            {
                FirstName = FirstName.Text,
                SecondName = SecondName.Text,
                Patronymic = Patronymic.Text,
                DateOfBirth = Convert.ToDateTime(DateOfBirth.Text),
                Email = Email.Text,
                PhoneNumber = PhoneNumber.Text,
                AddressID = address.AddressID,
                UserID = Login.UserID
            };
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserControls.PersonalAccount.PersonalAccountStackPanel.Visibility = Visibility.Visible;
            UserControls.PersonalAccount.PersonalInfoUC.Visibility = Visibility.Hidden;
        }

        //private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    ShowPersonalInfo();
        //}
    }
}
