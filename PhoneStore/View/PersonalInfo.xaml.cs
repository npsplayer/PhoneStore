using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
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
            Room.Text = String.Empty;
            Username.Text = String.Empty;
            Password.Password = String.Empty;
        }
        public void ShowPersonalInfo()
        {
            try
            {
                db = new OracleDbContext();
                Int32 userid = Login.UserID;
                var id = new OracleParameter("id", OracleDbType.Decimal, ParameterDirection.Input);
                id.Value = 26;
                var id_out = new OracleParameter("id_out", OracleDbType.Decimal, ParameterDirection.Output);
                var username_out = new OracleParameter("username_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                username_out.Size = 50;
                var password_out = new OracleParameter("password_out", OracleDbType.NVarchar2, Password.Password, ParameterDirection.Output);
                password_out.Size = 50;
                var city_out = new OracleParameter("city_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                city_out.Size = 50;
                var street_out = new OracleParameter("street_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                street_out.Size = 50;
                var hosenumber_out = new OracleParameter("hosenumber_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                hosenumber_out.Size = 50;
                var room_out = new OracleParameter("room_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                room_out.Size = 50;
                var firstname_out = new OracleParameter("firstname_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                firstname_out.Size = 50;
                var secondname_out = new OracleParameter("secondname_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                secondname_out.Size = 50;
                var patronymic_out = new OracleParameter("patronymic_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                patronymic_out.Size = 50;
                var dateofbirth_out = new OracleParameter("dateofbirth_out", OracleDbType.Date, ParameterDirection.Output);
                var email_out = new OracleParameter("email_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                email_out.Size = 50;
                var phonenumber_out = new OracleParameter("phonenumber_out", OracleDbType.NVarchar2, ParameterDirection.Output);
                phonenumber_out.Size = 50;

                var sql = "BEGIN PERSONALINFOSELECT(" + userid + ", :username_out, :password_out, :city_out, :street_out, :hosenumber_out, :room_out, :firstname_out, :secondname_out, :patronymic_out, :dateofbirth_out, :email_out, :phonenumber_out); END;";
                var checkuser = db.Database.ExecuteSqlCommand(sql, username_out, password_out, city_out, street_out, hosenumber_out, room_out, firstname_out, secondname_out, patronymic_out, dateofbirth_out, email_out, phonenumber_out);
                SaveButton.IsEnabled = true;

                FirstName.Text = firstname_out.Value.ToString();
                SecondName.Text = secondname_out.Value.ToString();
                Patronymic.Text = patronymic_out.Value.ToString();
                DateOfBirth.Text = dateofbirth_out.Value.ToString();
                Email.Text = email_out.Value.ToString();
                PhoneNumber.Text = phonenumber_out.Value.ToString();
                Street.Text = street_out.Value.ToString();
                HomeNumber.Text = hosenumber_out.Value.ToString();
                Room.Text = room_out.Value.ToString();
                Username.Text = username_out.Value.ToString();
                Password.Password = password_out.Value.ToString();
                City.Text = city_out.Value.ToString();
            }
            catch
            {
                PersonalInfoSnackBar.IsActive = true;
                SnackBarMessage.Content = "In order to view personal data, you must register or login to your account!";
            }
        }
        public void SaveInfo()
        {
            db = new OracleDbContext();
            var select1 = db.Customers.Where(cus => cus.UserID == View.Login.UserID).FirstOrDefault();
            if (Validation.ValidatePersonalInfo(FirstName, ErrorFirstName, IconFirstName,
                                               SecondName, ErrorSecondName, IconSecondName,
                                               Patronymic, ErrorPatronymic, IconPatronymic,
                                               DateOfBirth, ErrorDateOfBirth, IconDateOfBirth,
                                               City, ErrorCity, IconCity,
                                               Street,ErrorStreet, IconStreet,
                                               HomeNumber, ErrorHomeNumber, IconHomeNumber,
                                               Room, ErrorRoom, IconRoom,
                                               Email, ErrorEmail, IconEmail,
                                               PhoneNumber, ErrorPhoneNumber, IconPhoneNumber,
                                               Username, ErrorUsername, IconUsername,
                                               Password, ErrorPassword, IconPassword
                                               ))
            {
                if (select1 == null)
                {
                    Address address = new Address()
                    {
                        Street = Street.Text,
                        HouseNumber = HomeNumber.Text,
                        Room = Room.Text
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
                else if (select1 != null)
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
                    customerUpt.Address.Room = Room.Text;

                    User UserUpt = (from user in db.Users where user.UserID == select1.UserID select user).Single();
                    UserUpt.Username = Username.Text;
                    UserUpt.Password = Password.Password;

                    db.SaveChanges();
                    PersonalInfoSnackBar.IsActive = true;
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveInfo();
            
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
