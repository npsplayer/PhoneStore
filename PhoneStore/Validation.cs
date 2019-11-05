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
        public static bool ValidateRegisterAndLogin(TextBox Username, PackIcon IconErrorUsername, PasswordBox Password, PackIcon IconErrorPassword,
                                                    PasswordBox ConfirmPassword, PackIcon IconErrorConfirmPassword)
        {
            if(Username.Text == String.Empty)
            {
                
                Username.BorderBrush = new SolidColorBrush(Colors.Red);
                IconErrorUsername.Visibility = Visibility.Visible;
                IconErrorUsername.ToolTip = "The field must not be empty.";
            } else if(Username.Text.Length < 6 || Username.Text.Length > 15)
            {
                
                Username.BorderBrush = new SolidColorBrush(Colors.Red);
                IconErrorUsername.Visibility = Visibility.Visible;
                IconErrorUsername.ToolTip = "Value length should be from 6 to 15 characters";
            }
            else
            {
                IconErrorUsername.Visibility = Visibility.Hidden;
                Username.BorderBrush = new SolidColorBrush(Colors.Purple);
            }
            if(Password.Password == String.Empty)
            {
                Password.BorderBrush = new SolidColorBrush(Colors.Red);
                IconErrorPassword.Visibility = Visibility.Visible;
                IconErrorPassword.ToolTip = "The field must not be empty.";
            }
            else if (Password.Password.Length < 6 || Password.Password.Length > 15)
            {
                Password.BorderBrush = new SolidColorBrush(Colors.Red);
                IconErrorPassword.Visibility = Visibility.Visible;
                IconErrorPassword.ToolTip = "Value length should be from 6 to 15 characters";
            }
            else
            {
                IconErrorPassword.Visibility = Visibility.Hidden;
                Password.BorderBrush = new SolidColorBrush(Colors.Purple);
            }
            return true;
        }
    }
}
