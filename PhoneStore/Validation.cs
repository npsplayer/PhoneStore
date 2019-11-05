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
            if((Username.Text == String.Empty) || (Username.Text.Length < 6 || Username.Text.Length > 15) || (Username.Text.Any(char.IsWhiteSpace)))
            {
                IsValid = false;
                Username.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorUsername.Visibility = Visibility.Visible;
                IconUsername.Kind = PackIconKind.AlertCircleOutline;
                ErrorUsername.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorUsername.ToolTip = "The field must not contain an empty field.\n The field must not contain empty characters.\n Value length should be from 6 to 15 characters.";
            }
            else 
            {
                IsValid = true;
                ErrorUsername.Visibility = Visibility.Visible;
                ErrorUsername.Foreground = new SolidColorBrush(Colors.LightGreen);
                Username.BorderBrush = new SolidColorBrush(Color.FromRgb(118, 118, 118));
                IconUsername.Kind = PackIconKind.CheckCircleOutline;

            }
            if((Password.Password == String.Empty) || (Password.Password.Length < 6 || Password.Password.Length > 15) || Password.Password.Any(char.IsWhiteSpace))
            {
                IsValid = false;
                Password.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPassword.Visibility = Visibility.Visible;
                IconPassword.Kind = PackIconKind.AlertCircleOutline;
                ErrorPassword.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                ErrorPassword.ToolTip = "The field must not contain an empty field.\n The field must not contain empty characters.\n Value length should be from 6 to 15 characters.";
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
    }
}
