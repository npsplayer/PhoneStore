using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using PhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace PhoneStore.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public static int? CustID;
        public static int? UsID;
        public static int? AddrID;
        public static int? ProdID;
        public static int? PrOptID;
        public static int? PrID;
        public static int? OptID;
        OracleDbContext db = null;
        public AdminPanel()
        {
            InitializeComponent();
            ShowAll();
        }
        public void ShowAll()
        {
            db = new OracleDbContext();
            db.Customers.Load();
            db.Users.Load();
            db.Products.Load();
            db.Options.Load();
            db.ProductOptions.Load();
            DataGridUser.ItemsSource = db.Customers.Local;
            DataGridProduct.ItemsSource = db.Products.Local;
            DataGridOption.ItemsSource = db.ProductOptions.Local;
            NameProductOption.ItemsSource = db.Products.Local;
            Category.ItemsSource = db.Options.Local;
            
        }

        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customerDg = (DataGridUser.SelectedItem as Model.Customer);
            if (customerDg != null)
            {
                CustID = customerDg.CustomerID;
                UsID = customerDg.UserID;
                AddrID = customerDg.AddressID;
                Username.Text = customerDg.User.Username;
                Password.Text = customerDg.User.Password;
                Role.Text = customerDg.User.Role;
                FirstName.Text = customerDg.FirstName;
                SecondName.Text = customerDg.SecondName;
                Patronymic.Text = customerDg.Patronymic;
                Birthday.Text = Convert.ToString(customerDg.DateOfBirth);
                Email.Text = customerDg.Email;
                PhoneNumber.Text = customerDg.PhoneNumber;
                City.Text = customerDg.Address.City;
                Street.Text = customerDg.Address.Street;
                HouseNumber.Text = customerDg.Address.HouseNumber;
                Room.Text = customerDg.Address.Room;
                ErrorView();
            }

        }
        public void ErrorView()
        {
            ErrorUser.Visibility = Visibility.Hidden;
            Username.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorPassword.Visibility = Visibility.Hidden;
            Password.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorRole.Visibility = Visibility.Hidden;
            Role.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorFirstName.Visibility = Visibility.Hidden;
            FirstName.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorSecondName.Visibility = Visibility.Hidden;
            SecondName.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorPatronymic.Visibility = Visibility.Hidden;
            Patronymic.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorBirthday.Visibility = Visibility.Hidden;
            Birthday.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorEmail.Visibility = Visibility.Hidden;
            Email.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorPhoneNumber.Visibility = Visibility.Hidden;
            PhoneNumber.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorCity.Visibility = Visibility.Hidden;
            City.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorStreet.Visibility = Visibility.Hidden;
            Street.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorHouseNumber.Visibility = Visibility.Hidden;
            HouseNumber.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorRoom.Visibility = Visibility.Hidden;
            Room.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorName.Visibility = Visibility.Hidden;
            NameProduct.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorBrand.Visibility = Visibility.Hidden;
            Brand.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
            ErrorPrice.Visibility = Visibility.Hidden;
            Price.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
        }
        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            string pathFile = openFileDialog.FileName;
            FileStream fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imagecode = br.ReadBytes((Int32)fs.Length);
            var update = db.Customers.Where(c => c.CustomerID == CustID).FirstOrDefault();
            update.Photo = imagecode;
            db.SaveChanges();
            ShowAll();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var sql = "BEGIN ADMINDELETECUSTOMER(" + UsID + "," + AddrID + "," + CustID + "); END;";
                var update = db.Database.ExecuteSqlCommand(sql);
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Delete successful!";
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Error!";
            }

        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.ValidateAdminCustomer(FirstName, ErrorFirstName, IconFirstName,
                                                 SecondName, ErrorSecondName, IconSecondName,
                                                 Patronymic, ErrorPatronymic, IconPatronymic,
                                                 Birthday, ErrorBirthday, IconBirthday,
                                                 City, ErrorCity, IconCity,
                                                 Street, ErrorStreet, IconStreet,
                                                 HouseNumber, ErrorHouseNumber, IconHouseNumber,
                                                 Room, ErrorRoom, IconRoom,
                                                 Email, ErrorEmail, IconEmail,
                                                 PhoneNumber, ErrorPhoneNumber, IconPhoneNumber,
                                                 Username, ErrorUser, IconUser,
                                                 Password, ErrorPassword, IconPassword, 
                                                 Role, ErrorRole, IconRole))
            {
                try
                {
                    var USERNAME = new OracleParameter("Username", OracleDbType.NVarchar2, Username.Text, ParameterDirection.Input);
                    var PASSWORD = new OracleParameter("Password", OracleDbType.NVarchar2, Password.Text, ParameterDirection.Input);
                    var ROLE = new OracleParameter("Role", OracleDbType.NVarchar2, Role.Text, ParameterDirection.Input);
                    var CITY = new OracleParameter("City", OracleDbType.NVarchar2, City.Text, ParameterDirection.Input);
                    var STREET = new OracleParameter("Street", OracleDbType.NVarchar2, Street.Text, ParameterDirection.Input);
                    var HOUSENUMBER = new OracleParameter("HouseNumber", OracleDbType.NVarchar2, HouseNumber.Text, ParameterDirection.Input);
                    var ROOM = new OracleParameter("Room", OracleDbType.NVarchar2, Room.Text, ParameterDirection.Input);
                    var FIRSTNAME = new OracleParameter("Firstname", OracleDbType.NVarchar2, FirstName.Text, ParameterDirection.Input);
                    var SECONDNAME = new OracleParameter("Secondname", OracleDbType.NVarchar2, SecondName.Text, ParameterDirection.Input);
                    var PATRONYMIC = new OracleParameter("Patronymic", OracleDbType.NVarchar2, Patronymic.Text, ParameterDirection.Input);
                    var DATEOFBIRTH = new OracleParameter("DateOfBirth", OracleDbType.Date, Convert.ToDateTime(Birthday.Text), ParameterDirection.Input);
                    var EMAIL = new OracleParameter("Email", OracleDbType.NVarchar2, Email.Text, ParameterDirection.Input);
                    var PHONENUMBER = new OracleParameter("PhoneNumber", OracleDbType.NVarchar2, PhoneNumber.Text, ParameterDirection.Input);
                    var sql = "BEGIN ADMINADDCUSTOMER(:Username, :Password, :Role, :City, :Street, :HouseNumber, :Room, :Firstname, :Secondname, :Patronymic, :DateOfBirth, :Email, :PhoneNumber); END;";

                    db.Database.ExecuteSqlCommand(sql, USERNAME, PASSWORD, ROLE, CITY, STREET, HOUSENUMBER, ROOM, FIRSTNAME, SECONDNAME, PATRONYMIC, DATEOFBIRTH, EMAIL, PHONENUMBER);
                    ShowAll();
                    ErrorView();
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Add successful!";

                }
                catch
                {
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Check the correctness of the entered data!";
                }
            }
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.ValidateAdminCustomer(FirstName, ErrorFirstName, IconFirstName,
                                                 SecondName, ErrorSecondName, IconSecondName,
                                                 Patronymic, ErrorPatronymic, IconPatronymic,
                                                 Birthday, ErrorBirthday, IconBirthday,
                                                 City, ErrorCity, IconCity,
                                                 Street, ErrorStreet, IconStreet,
                                                 HouseNumber, ErrorHouseNumber, IconHouseNumber,
                                                 Room, ErrorRoom, IconRoom,
                                                 Email, ErrorEmail, IconEmail,
                                                 PhoneNumber, ErrorPhoneNumber, IconPhoneNumber,
                                                 Username, ErrorUser, IconUser,
                                                 Password, ErrorPassword, IconPassword,
                                                 Role, ErrorRole, IconRole))
            {
                try
                {
                    var USERNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Username.Text, ParameterDirection.InputOutput);
                    var PASSWORD = new OracleParameter("PASSWORD", OracleDbType.NVarchar2, Password.Text, ParameterDirection.InputOutput);
                    var ROLE = new OracleParameter("Role", OracleDbType.NVarchar2, Role.Text, ParameterDirection.Input);
                    var CITY = new OracleParameter("CITY", OracleDbType.NVarchar2, City.Text, ParameterDirection.InputOutput);
                    var STREET = new OracleParameter("STREET", OracleDbType.NVarchar2, Street.Text, ParameterDirection.InputOutput);
                    var HOMENUMBER = new OracleParameter("HOUSENUMBER", OracleDbType.NVarchar2, HouseNumber.Text, ParameterDirection.InputOutput);
                    var ROOM = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Room.Text, ParameterDirection.InputOutput);
                    var FIRSTNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, FirstName.Text, ParameterDirection.InputOutput);
                    var SECONDNAME = new OracleParameter("USERNAME", OracleDbType.NVarchar2, SecondName.Text, ParameterDirection.InputOutput);
                    var PATRONYMIC = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Patronymic.Text, ParameterDirection.InputOutput);
                    var DATEOFBIRTH = new OracleParameter("USERNAME", OracleDbType.Date, Convert.ToDateTime(Birthday.Text), ParameterDirection.InputOutput);
                    var EMAIL = new OracleParameter("USERNAME", OracleDbType.NVarchar2, Email.Text, ParameterDirection.InputOutput);
                    var PHONENUMBER = new OracleParameter("USERNAME", OracleDbType.NVarchar2, PhoneNumber.Text, ParameterDirection.InputOutput);
                    var sql = "BEGIN ADMINUPDATECUSTOMER(" + UsID + ", :USERNAME, :PASSWORD, :Role," + AddrID + ",:CITY, :STREET, :HOMENUMBER, :ROOM, :FIRSTNAME, :SECONDNAME, :PATRONYMIC, :DATEOFBIRTH, :EMAIL, :PHONENUMBER); END;";
                    var update = db.Database.ExecuteSqlCommand(sql, USERNAME, PASSWORD, ROLE, CITY, STREET, HOMENUMBER, ROOM, FIRSTNAME, SECONDNAME, PATRONYMIC, DATEOFBIRTH, EMAIL, PHONENUMBER);
                    ShowAll();
                    SnackBar.IsActive = true;
                    ErrorView();
                    SnackBarMessage.Content = "Update successful!";
                }
                catch
                {
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Check the correctness of the entered data!";
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sql = "BEGIN ADMINDELETEPRODUCT(" + ProdID + "); END;";
                var update = db.Database.ExecuteSqlCommand(sql);
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Delete successful!";
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Error!";
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.ValidateAdminProduct(NameProduct, ErrorName, IconName, Brand, ErrorBrand, IconBrand, Price, ErrorPrice, IconPrice))
            {
                try
                {
                    var NAME = new OracleParameter("NAME", OracleDbType.NVarchar2, NameProduct.Text, ParameterDirection.Input);
                    var BRAND = new OracleParameter("BRAND", OracleDbType.NVarchar2, Brand.Text, ParameterDirection.Input);
                    var PRICE = new OracleParameter("PRICE", OracleDbType.Decimal, Convert.ToSingle(Price.Text), ParameterDirection.Input);

                    var sql = "BEGIN ADMINUPDATEPRODUCT(" + ProdID + ", :NAME, :BRAND, :PRICE); END;";
                    var update = db.Database.ExecuteSqlCommand(sql, NAME, BRAND, PRICE);
                    ShowAll();
                    SnackBar.IsActive = true;
                    ErrorView();
                    SnackBarMessage.Content = "Update successful!";
                }
                catch
                {
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Check the correctness of the entered data!";
                }
            }
        }

        private void AddPruduct_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.ValidateAdminProduct(NameProduct, ErrorName, IconName, Brand, ErrorBrand, IconBrand, Price, ErrorPrice, IconPrice))
            {
                try
                {
                    var NAME = new OracleParameter("NAME", OracleDbType.NVarchar2, NameProduct.Text, ParameterDirection.Input);
                    var BRAND = new OracleParameter("BRAND", OracleDbType.NVarchar2, Brand.Text, ParameterDirection.Input);
                    var PRICE = new OracleParameter("PRICE", OracleDbType.Decimal, Convert.ToSingle(Price.Text), ParameterDirection.Input);

                    var sql = "BEGIN ADMINADDPRODUCT(:NAME, :BRAND, :PRICE); END;";
                    var update = db.Database.ExecuteSqlCommand(sql, NAME, BRAND, PRICE);
                    ShowAll();
                    ErrorView();
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Add successful!";
                }
                catch
                {
                    SnackBar.IsActive = true;
                    SnackBarMessage.Content = "Check the correctness of the entered data!";
                }
            }
        }

        private void PhotoProduct_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            string pathFile = openFileDialog.FileName;
            FileStream fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imagecode = br.ReadBytes((Int32)fs.Length);
            var update = db.Products.Where(p => p.ProductID == ProdID).FirstOrDefault();
            update.Photo = imagecode;
            db.SaveChanges();
            ShowAll();
        }

        private void DataGridProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ProductDg = (DataGridProduct.SelectedItem as Model.Product);
            if (ProductDg != null)
            {
                ProdID = ProductDg.ProductID;
                NameProduct.Text = ProductDg.Name;
                Brand.Text = ProductDg.Manufacturer;
                Price.Text = Convert.ToString(ProductDg.Price);
            }
        }

        private void DataGridOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var optionDg = (DataGridOption.SelectedItem as Model.ProductOption);
            if(optionDg != null)
            {

                NameProductOption.Text = optionDg.Product.Name;
                Category.Text = optionDg.Option.OptionName;
                Value.Text = optionDg.Value;
                Unit.Text = optionDg.Unit;
                PrOptID = optionDg.ProductOptionID;
                PrID = optionDg.ProductID;
                OptID = optionDg.OptionID;
            }
        }

        private void AddOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var VALUE = new OracleParameter("VALUE", OracleDbType.NVarchar2, Value.Text, ParameterDirection.Input);
                var UNIT = new OracleParameter("UNIT", OracleDbType.NVarchar2, Unit.Text, ParameterDirection.Input);
                NameProductOption.DisplayMemberPath = "ProductID";
                Category.DisplayMemberPath = "OptionID";
                int PrID = Convert.ToInt32(NameProductOption.Text);
                int OptID = Convert.ToInt32(Category.Text);
                var sql = "BEGIN ADMINADDOPTION("+ PrID + "," + OptID +",:VALUE, :UNIT); END;";
                var update = db.Database.ExecuteSqlCommand(sql, VALUE, UNIT);
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Add successful!";
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Check the correctness of the entered data!";
            }
        }

        private void UpdateOption_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            var VALUE = new OracleParameter("VALUE", OracleDbType.NVarchar2, Value.Text, ParameterDirection.Input);
                var UNIT = new OracleParameter("UNIT", OracleDbType.NVarchar2, Unit.Text, ParameterDirection.Input);

                var sql = "BEGIN ADMINUPDATEOPTION("+ PrOptID + "," + PrID + "," + OptID + ",:VALUE, :UNIT); END;";
                var update = db.Database.ExecuteSqlCommand(sql, VALUE, UNIT);
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Update successful!";
            }
            catch
            {
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Check the correctness of the entered data!";
            }
        }

        private void DeleteOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sql = "BEGIN ADMINDELETEOPTION(" + PrOptID + "); END;";
                var update = db.Database.ExecuteSqlCommand(sql);
                ShowAll();
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Delete successful!";
            }
            catch
            {
                SnackBar.IsActive = true;
                SnackBarMessage.Content = "Error!";
            }
        }
    }
}
