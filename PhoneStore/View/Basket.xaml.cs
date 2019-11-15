using PhoneStore.Model;
using PhoneStore.UserControls;
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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        OracleDbContext db = null;
        int value = 0;
        ShowCatalog catalogView;
        public static int productID;
        public int customer = Login.CustomerID;
        public Basket()
        {
            InitializeComponent();
            db = new OracleDbContext();
            db.Products.Load();
            db.Baskets.Load();
            var select = db.Baskets.Where(bask => bask.CustomerID == customer);
            ListViewBasket.ItemsSource = select.ToList();
        }
        public Basket (UserControl userControl)
        {
            userControl = catalogView;
            InitializeComponent();
            db = new OracleDbContext();
            

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void ShowBasket()
        {
            Model.Basket basket = new Model.Basket()
            {
                ProductID = productID,
                CustomerID = Login.CustomerID,
                Amount = 1
            };
            db.Baskets.Add(basket);
            db.SaveChanges();
        }
        private void PlusProduct_Click(object sender, RoutedEventArgs e)
        {
            
            //value++;
            //CurrentAmount.Text = Convert.ToString(value);
        }

        private void MinusProduct_Click(object sender, RoutedEventArgs e)
        {
            //CurrentAmount = FindVisualChildByName<TextBox>(ListViewBasket, "CurrentAmount");
            //value--;
            //CurrentAmount.Text = Convert.ToString(value);
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
 
 

    }
}
