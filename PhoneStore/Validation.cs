using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PhoneStore
{
    class Validation
    {
        public static bool ValidateRegisterAndLogin(TextBox Username, TextBlock ErrorUsername, PackIcon IconUsername, 
                                                    PasswordBox Password, TextBlock ErrorPassword, PackIcon IconPassword, 
                                                    PasswordBox ConfirmPassword, TextBlock ErrorConfirmPassword, PackIcon IconConfirmPassword)
        {
            bool IsValid = false;
            if((Username.Text == String.Empty) || (Username.Text.Length < 6 || Username.Text.Length > 20) || (Username.Text.Any(char.IsWhiteSpace)))
            {
                IsValid = false;
                Username.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorUsername.Visibility = Visibility.Visible;
                IconUsername.Kind = PackIconKind.AlertCircleOutline;
                ErrorUsername.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorUsername.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be from 6 to 20 characters.";
            }
            else 
            {
                IsValid = true;
                ErrorUsername.Visibility = Visibility.Visible;
                ErrorUsername.Foreground = new SolidColorBrush(Colors.LightGreen);
                Username.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconUsername.Kind = PackIconKind.CheckCircleOutline;

            }
            if((Password.Password == String.Empty) || (Password.Password.Length < 6 || Password.Password.Length > 20) || Password.Password.Any(char.IsWhiteSpace))
            {
                IsValid = false;
                Password.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPassword.Visibility = Visibility.Visible;
                IconPassword.Kind = PackIconKind.AlertCircleOutline;
                ErrorPassword.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPassword.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be from 6 to 20 characters.";
                if (ConfirmPassword != null)
                {
                    if (ConfirmPassword.Password != String.Empty)
                    {
                        if (Password.Password != ConfirmPassword.Password)
                        {
                            IconConfirmPassword.Visibility = Visibility.Visible;
                            ErrorConfirmPassword.Visibility = Visibility.Visible;
                            ErrorConfirmPassword.ToolTip = "Passwords do not match.";
                            IconConfirmPassword.Kind = PackIconKind.AlertCircleOutline;
                            ConfirmPassword.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                            ErrorConfirmPassword.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                        }
                        else if (Password.Password == ConfirmPassword.Password)
                        {
                            IconConfirmPassword.Visibility = Visibility.Visible;
                            ErrorConfirmPassword.Visibility = Visibility.Visible;
                            ErrorConfirmPassword.Foreground = new SolidColorBrush(Colors.LightGreen);
                            ConfirmPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                            IconConfirmPassword.Kind = PackIconKind.CheckCircleOutline;
                        }
                        
                    }
                    else
                    {
                        IconConfirmPassword.Visibility = Visibility.Visible;
                        ErrorConfirmPassword.Visibility = Visibility.Visible;
                        ErrorConfirmPassword.ToolTip = "The field must not contain an empty field.";
                        IconConfirmPassword.Kind = PackIconKind.AlertCircleOutline;
                        ConfirmPassword.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                        ErrorConfirmPassword.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                    }
                }
            }
            else
            {
                IsValid = true;
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                ErrorPassword.Foreground = new SolidColorBrush(Colors.LightGreen);
                IconPassword.Kind = PackIconKind.CheckCircleOutline;
            }
            return IsValid;
        }

        public static bool ValidatePersonalInfo(TextBox FirstName, TextBlock ErrorFirstName, PackIcon IconFirstName,
                                                TextBox Secondname, TextBlock ErrorSecondName, PackIcon IconSecontName,
                                                TextBox Patronymic, TextBlock ErrorPatronymic, PackIcon IconPatronymic,
                                                DatePicker DateOfBirth, TextBlock ErrorDateOfBirth, PackIcon IconDateOfBirth,
                                                TextBox City, TextBlock ErrorCity, PackIcon IconCity,
                                                TextBox Street, TextBlock ErrorStreet, PackIcon IconStreet,
                                                TextBox HomeNumber, TextBlock ErrorHomeNumber, PackIcon IconHomeNumber,
                                                TextBox Room, TextBlock ErrorRoom, PackIcon IconRoom,
                                                TextBox Email, TextBlock ErrorEmail, PackIcon IconEmail,
                                                TextBox PhoneNumber, TextBlock ErrorPhoneNumber, PackIcon IconPhoneNumber,
                                                TextBox Username, TextBlock ErrorUsername, PackIcon IconUsername,
                                                PasswordBox Password, TextBlock ErrorPassword, PackIcon IconPassword)

        {
            bool IsValid = false;
            if (Validation.ValidateRegisterAndLogin(Username, ErrorUsername, IconUsername, Password, ErrorPassword, IconPassword, null, null, null))
            {
                IsValid = true;
            }
            if (FirstName.Text == String.Empty || FirstName.Text.Length > 20 || !FirstName.Text.Any(char.IsLetter) || 
               FirstName.Text.Any(char.IsWhiteSpace) || FirstName.Text.Any(char.IsNumber))
            {
                IsValid = false;
                FirstName.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorFirstName.Visibility = Visibility.Visible;
                IconFirstName.Kind = PackIconKind.AlertCircleOutline;
                ErrorFirstName.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorFirstName.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 20 characters.\nCannot contain numbers.";
            }
            else
            {
                IsValid = true;
                ErrorFirstName.Visibility = Visibility.Visible;
                ErrorFirstName.Foreground = new SolidColorBrush(Colors.LightGreen);
                FirstName.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconFirstName.Kind = PackIconKind.CheckCircleOutline;
            }
            if (Secondname.Text == String.Empty || Secondname.Text.Length > 20 || !Secondname.Text.Any(char.IsLetter) ||
               Secondname.Text.Any(char.IsWhiteSpace) || Secondname.Text.Any(char.IsNumber))
            {
                IsValid = false;
                Secondname.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorSecondName.Visibility = Visibility.Visible;
                IconSecontName.Kind = PackIconKind.AlertCircleOutline;
                ErrorSecondName.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorSecondName.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 20 characters.\nCannot contain numbers.";
            }
            else
            {
                IsValid = true;
                ErrorSecondName.Visibility = Visibility.Visible;
                ErrorSecondName.Foreground = new SolidColorBrush(Colors.LightGreen);
                Secondname.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconSecontName.Kind = PackIconKind.CheckCircleOutline;
            }
            if (Patronymic.Text == String.Empty || Patronymic.Text.Length > 20 || !Patronymic.Text.Any(char.IsLetter) ||
               Patronymic.Text.Any(char.IsWhiteSpace) || Patronymic.Text.Any(char.IsNumber))
            {
                IsValid = false;
                Patronymic.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPatronymic.Visibility = Visibility.Visible;
                IconPatronymic.Kind = PackIconKind.AlertCircleOutline;
                ErrorPatronymic.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPatronymic.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 20 characters.\nCannot contain numbers.";
            }
            else
            {
                IsValid = true;
                ErrorPatronymic.Visibility = Visibility.Visible;
                ErrorPatronymic.Foreground = new SolidColorBrush(Colors.LightGreen);
                Patronymic.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconPatronymic.Kind = PackIconKind.CheckCircleOutline;
            }
            DateTime date = Convert.ToDateTime(DateOfBirth.Text);
            DateTime mindate = Convert.ToDateTime("01.01.1900");
            DateTime maxdate = Convert.ToDateTime(DateTime.Now);
            if (DateOfBirth.Text == String.Empty  || DateOfBirth.Text.Any(char.IsLetter) ||
               DateOfBirth.Text.Any(char.IsWhiteSpace) || !DateOfBirth.Text.Any(char.IsNumber) || (date < mindate || date > maxdate))
            {
                IsValid = false;
                DateOfBirth.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorDateOfBirth.Visibility = Visibility.Visible;
                IconDateOfBirth.Kind = PackIconKind.AlertCircleOutline;
                ErrorDateOfBirth.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorDateOfBirth.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nIncorrect date entry (from 01.01.1900 to "+ 
                    maxdate.ToShortDateString() + ").\nCannot contain letters.";
            }
            else
            {
                IsValid = true;
                ErrorDateOfBirth.Visibility = Visibility.Visible;
                ErrorDateOfBirth.Foreground = new SolidColorBrush(Colors.LightGreen);
                DateOfBirth.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconDateOfBirth.Kind = PackIconKind.CheckCircleOutline;
            }
            if (City.Text == String.Empty || City.Text.Length > 20 || !City.Text.Any(char.IsLetter) ||
               City.Text.Any(char.IsWhiteSpace) || City.Text.Any(char.IsNumber))
            {
                IsValid = false;
                City.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorCity.Visibility = Visibility.Visible;
                IconCity.Kind = PackIconKind.AlertCircleOutline;
                ErrorCity.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorCity.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 20 characters.\nCannot contain numbers.";
            }
            else
            {
                IsValid = true;
                ErrorCity.Visibility = Visibility.Visible;
                ErrorCity.Foreground = new SolidColorBrush(Colors.LightGreen);
                City.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconCity.Kind = PackIconKind.CheckCircleOutline;
            }
            if (Street.Text == String.Empty || Street.Text.Length > 20 || !Street.Text.Any(char.IsLetter) ||
               Street.Text.Any(char.IsWhiteSpace) || Street.Text.Any(char.IsNumber))
            {
                IsValid = false;
                Street.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorStreet.Visibility = Visibility.Visible;
                IconStreet.Kind = PackIconKind.AlertCircleOutline;
                ErrorStreet.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorStreet.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 20 characters.\nCannot contain numbers.";
            }
            else
            {
                IsValid = true;
                ErrorStreet.Visibility = Visibility.Visible;
                ErrorStreet.Foreground = new SolidColorBrush(Colors.LightGreen);
                Street.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconStreet.Kind = PackIconKind.CheckCircleOutline;
            }
            if (HomeNumber.Text == String.Empty || HomeNumber.Text.Length > 5 || HomeNumber.Text.Any(char.IsLetter) ||
               HomeNumber.Text.Any(char.IsWhiteSpace) || !HomeNumber.Text.Any(char.IsNumber))
            {
                IsValid = false;
                HomeNumber.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorHomeNumber.Visibility = Visibility.Visible;
                IconHomeNumber.Kind = PackIconKind.AlertCircleOutline;
                ErrorHomeNumber.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorHomeNumber.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 5 characters.\nCannot contain letters.";
            }
            else
            {
                IsValid = true;
                ErrorHomeNumber.Visibility = Visibility.Visible;
                ErrorHomeNumber.Foreground = new SolidColorBrush(Colors.LightGreen);
                HomeNumber.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconHomeNumber.Kind = PackIconKind.CheckCircleOutline;
            }
            if (Room.Text == String.Empty || Room.Text.Length > 5 || Room.Text.Any(char.IsLetter) ||
               Room.Text.Any(char.IsWhiteSpace) || !Room.Text.Any(char.IsNumber))
            {
                IsValid = false;
                Room.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorRoom.Visibility = Visibility.Visible;
                IconRoom.Kind = PackIconKind.AlertCircleOutline;
                ErrorRoom.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorRoom.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 5 characters.\nCannot contain letters.";
            }
            else
            {
                IsValid = true;
                ErrorRoom.Visibility = Visibility.Visible;
                ErrorRoom.Foreground = new SolidColorBrush(Colors.LightGreen);
                Room.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconRoom.Kind = PackIconKind.CheckCircleOutline;
            }
            if (Email.Text == String.Empty || Email.Text.Length > 30 || Email.Text.Any(char.IsWhiteSpace) || 
                (!Email.Text.Contains("@gmail.com") && !Email.Text.Contains("@mail.ru") && !Email.Text.Contains("@yandex.ru")))
            {
                IsValid = false;
                Email.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorEmail.Visibility = Visibility.Visible;
                IconEmail.Kind = PackIconKind.AlertCircleOutline;
                ErrorEmail.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorEmail.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be to 30 characters.\nMust contain '@gmail.com' or '@mail.ru' or '@yandex.ru'.";
            }
            else
            {
                IsValid = true;
                ErrorEmail.Visibility = Visibility.Visible;
                ErrorEmail.Foreground = new SolidColorBrush(Colors.LightGreen);
                Email.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconEmail.Kind = PackIconKind.CheckCircleOutline;
            }
            if (PhoneNumber.Text == String.Empty || PhoneNumber.Text.Length > 15 || PhoneNumber.Text.Length < 10 || PhoneNumber.Text.Any(char.IsLetter) ||
               PhoneNumber.Text.Any(char.IsWhiteSpace) || !PhoneNumber.Text.Any(char.IsNumber))
            {
                IsValid = false;
                PhoneNumber.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                IconPhoneNumber.Kind = PackIconKind.AlertCircleOutline;
                ErrorPhoneNumber.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPhoneNumber.ToolTip = "The field must not contain an empty field.\nCannot contain empty characters.\nValue length should be from 10 to 15 characters.\nCannot contain letters.";
            }
            else
            {
                IsValid = true;
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                ErrorPhoneNumber.Foreground = new SolidColorBrush(Colors.LightGreen);
                PhoneNumber.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconPhoneNumber.Kind = PackIconKind.CheckCircleOutline;
            }
            
            return IsValid;
        }
    }
}
