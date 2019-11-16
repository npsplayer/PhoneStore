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
        public static Button SaveBut;
        int userid = Login.UserID;
        int addressid = Login.AddressID;
        public static int customerID;
        public PersonalInfo()
        {
            InitializeComponent();
            CheckNull();
            ShowPersonalInfo();
            SaveBut = SaveButton;
        }
        
        public void CheckNull()
        {
            if (FirstName.Text == "null" || FirstName.Text == "") { FirstName.Text = String.Empty; }
            if (SecondName.Text == "null" || SecondName.Text == "") { SecondName.Text = String.Empty; }
            if (Patronymic.Text == "null" || Patronymic.Text == "") { Patronymic.Text = String.Empty; }
            if (DateOfBirth.Text == "null" || DateOfBirth.Text == "") { DateOfBirth.Text = String.Empty; }
            if (Email.Text == "null" || Email.Text == "") { Email.Text = String.Empty; }
            if (PhoneNumber.Text == "null" || PhoneNumber.Text == "") { PhoneNumber.Text = String.Empty; }
            if (Street.Text == "null" || Street.Text == "") { Street.Text = String.Empty; }
            if (City.Text == "null" || City.Text == "") { City.Text = String.Empty; }
            if (HomeNumber.Text == "null" || HomeNumber.Text == "") { HomeNumber.Text = String.Empty; }
            if (Room.Text == "null" || Room.Text == "") { Room.Text = String.Empty; }
            if (Username.Text == "null" || Username.Text == "") { Username.Text = String.Empty; }
            if (Password.Password == "null" || Password.Password == "") { Password.Password = String.Empty; }
        }
        public void ShowPersonalInfo()
        {
            try
            {
                db = new OracleDbContext();
                var id_out = new OracleParameter("id_out", OracleDbType.Decimal, ParameterDirection.Output);
                var username_out = new OracleParameter("username_out", OracleDbType.NVarchar2, ParameterDirection.Output); username_out.Size = 50;
                var password_out = new OracleParameter("password_out", OracleDbType.NVarchar2, Password.Password, ParameterDirection.Output); password_out.Size = 50;
                var city_out = new OracleParameter("city_out", OracleDbType.NVarchar2, ParameterDirection.Output); city_out.Size = 50;
                var street_out = new OracleParameter("street_out", OracleDbType.NVarchar2, ParameterDirection.Output); street_out.Size = 50;
                var homenumber_out = new OracleParameter("homenumber_out", OracleDbType.NVarchar2, ParameterDirection.Output); homenumber_out.Size = 50;
                var room_out = new OracleParameter("room_out", OracleDbType.NVarchar2, ParameterDirection.Output); room_out.Size = 50;
                var firstname_out = new OracleParameter("firstname_out", OracleDbType.NVarchar2, ParameterDirection.Output); firstname_out.Size = 50;
                var secondname_out = new OracleParameter("secondname_out", OracleDbType.NVarchar2, ParameterDirection.Output); secondname_out.Size = 50;
                var patronymic_out = new OracleParameter("patronymic_out", OracleDbType.NVarchar2, ParameterDirection.Output); patronymic_out.Size = 50;
                var dateofbirth_out = new OracleParameter("dateofbirth_out", OracleDbType.Date, ParameterDirection.Output);
                var email_out = new OracleParameter("email_out", OracleDbType.NVarchar2, ParameterDirection.Output); email_out.Size = 50;
                var phonenumber_out = new OracleParameter("phonenumber_out", OracleDbType.NVarchar2, ParameterDirection.Output); phonenumber_out.Size = 50;

                var sql = "BEGIN PERSONALINFOSELECT(" + userid + ", :username_out, :password_out, :city_out, :street_out, :homenumber_out, :room_out, :firstname_out, :secondname_out, :patronymic_out, :dateofbirth_out, :email_out, :phonenumber_out); END;";
                var select = db.Database.ExecuteSqlCommand(sql, username_out, password_out, city_out, street_out, homenumber_out, room_out, firstname_out, secondname_out, patronymic_out, dateofbirth_out, email_out, phonenumber_out);
                SaveButton.IsEnabled = true;
               
                FirstName.Text = firstname_out.Value.ToString();
                SecondName.Text = secondname_out.Value.ToString();
                Patronymic.Text = patronymic_out.Value.ToString();
                DateOfBirth.Text = dateofbirth_out.Value.ToString();
                Email.Text = email_out.Value.ToString();
                PhoneNumber.Text = phonenumber_out.Value.ToString();
                Street.Text = street_out.Value.ToString();
                HomeNumber.Text = homenumber_out.Value.ToString();
                Room.Text = room_out.Value.ToString();
                Username.Text = username_out.Value.ToString();
                Password.Password = password_out.Value.ToString();
                City.Text = city_out.Value.ToString();
                CheckNull();
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
                try
                {
                    var USERNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Username.Text, ParameterDirection.InputOutput);
                    var PASSWORD = new OracleParameter("PASSWORD", OracleDbType.NVarchar2, Password.Password, ParameterDirection.InputOutput);
                    var CITY = new OracleParameter("CITY", OracleDbType.NVarchar2, City.Text, ParameterDirection.InputOutput);
                    var STREET = new OracleParameter("STREET", OracleDbType.NVarchar2, Street.Text, ParameterDirection.InputOutput);
                    var HOMENUMBER = new OracleParameter("HOUSENUMBER", OracleDbType.NVarchar2, HomeNumber.Text, ParameterDirection.InputOutput);
                    var ROOM = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Room.Text, ParameterDirection.InputOutput);
                    var FIRSTNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, FirstName.Text, ParameterDirection.InputOutput);
                    var SECONDNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, SecondName.Text, ParameterDirection.InputOutput);
                    var PATRONYMIC = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Patronymic.Text, ParameterDirection.InputOutput);
                    var DATEOFBIRTH = new OracleParameter("USERNAME", OracleDbType.Date, Convert.ToDateTime(DateOfBirth.Text), ParameterDirection.InputOutput);
                    var EMAIL = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Email.Text, ParameterDirection.InputOutput);
                    var PHONENUMBER = new OracleParameter("USERNAME", OracleDbType.NVarchar2, PhoneNumber.Text, ParameterDirection.InputOutput);
                    var sql = "BEGIN PERSONAINFOUPDATE(" + userid + ", :USERNAME, :PASSWORD," + addressid + ",:CITY, :STREET, :HOMENUMBER, :ROOM, :FIRSTNAME, :SECONDNAME, :PATRONYMIC, :DATEOFBIRTH, :EMAIL, :PHONENUMBER); END;";
                    var update = db.Database.ExecuteSqlCommand(sql, USERNAME, PASSWORD, CITY, STREET, HOMENUMBER, ROOM, FIRSTNAME, SECONDNAME, PATRONYMIC, DATEOFBIRTH, EMAIL, PHONENUMBER);
                    PersonalInfoSnackBar.IsActive = true;
                }
                catch
                {
                    PersonalInfoSnackBar.IsActive = true;
                    SnackBarMessage.Content = "Ooopss... Try again?";
                }
                
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveInfo();
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CheckNull();
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            PersonalInfoSnackBar.IsActive = false;
        }
    }
}
