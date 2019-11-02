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
using System.Windows.Shapes;

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для PersonalInfo.xaml
    /// </summary>
    public partial class PersonalInfo : Window
    {
        OracleDbContext db = null;
        View.PersonalInfo personalInfo;
        public static Button SaveBut;
        public PersonalInfo()
        {

            InitializeComponent();
            db = new OracleDbContext();
            ClearInfo();
            ShowPersonalInfo();
            SaveBut = SaveButton;
        }
        public PersonalInfo(View.PersonalInfo personal)
        {
            InitializeComponent();
            db = new OracleDbContext();
            personalInfo = personal;
            ClearInfo();
            ShowPersonalInfo();
            SaveBut = SaveButton;
        }

        public void ClearInfo()
        {
            FirstName.Text = "";
            SecondName.Text = String.Empty;
            Patronymic.Text = String.Empty;
            DateOfBirth.Text = String.Empty;
            Email.Text = String.Empty;
            PhoneNumber.Text = String.Empty;
            Street.Text = String.Empty;
            HomeNumber.Text = String.Empty;
            Entrance.Text = String.Empty;
            Username.Text = String.Empty;
            Password.Password = String.Empty;
        }
        public void ShowPersonalInfo()
        {
            db = new OracleDbContext();
            db.Customers.Load();
            db.Addresses.Load();
            db.Users.Load();
            var select1 = db.Customers.Where(cus => cus.UserID == View.Login.UserID).FirstOrDefault();
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
                Username.Text = select1.User.Username;
                Password.Password = select1.User.Password;
                SaveBut.IsEnabled = true;
            } 
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            var select1 = db.Customers.Where(cus => cus.UserID == View.Login.UserID).FirstOrDefault();
            if (select1 == null)
            {
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
                    UserID = Login.UserID,
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                PersonalInfoSnackBar.IsActive = true;
            }
            else if(select1 != null)
            {
                Customer customerUpt = (from cust in db.Customers where cust.UserID == select1.UserID select cust).FirstOrDefault();
                customerUpt.FirstName = FirstName.Text;
                customerUpt.SecondName = SecondName.Text;
                customerUpt.Patronymic = Patronymic.Text;
                customerUpt.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                customerUpt.Email = Email.Text;
                customerUpt.PhoneNumber = PhoneNumber.Text;
                customerUpt.Address.Street = Street.Text;
                customerUpt.Address.HouseNumber = HomeNumber.Text;
                customerUpt.Address.Entrance = Entrance.Text;

                User UserUpt = (from user in db.Users where user.UserID == select1.UserID select user).Single();
                UserUpt.Username = Username.Text;
                UserUpt.Password = Password.Password;

                db.SaveChanges();
                PersonalInfoSnackBar.IsActive = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInfo();
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            PersonalInfoSnackBar.IsActive = false;
        }
    }
}
