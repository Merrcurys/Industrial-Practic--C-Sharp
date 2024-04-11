using System;
using System.Collections.Generic;
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

namespace Vinyl
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public AccountPage()
        {
            InitializeComponent();

            AccDgr.ItemsSource = db.Accounts.ToList();
        }

        private void AccDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (AccDgr.SelectedItem != null)
                {
                    var strochka = AccDgr.SelectedItem as Accounts;
                    LoginTbx.Text = strochka.Login;
                    PasswordTbx.Password = strochka.Password;
                }
            }
            catch { MessageBox.Show("Ошибка!"); }
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                Accounts acc = new Accounts();
                if (LoginTbx.Text.Length > 0 && PasswordTbx.Password.Length > 0 && PasswordTbx.Password.Length <= 25  && LoginTbx.Text.Length <= 25)
                {
                    acc.Login = LoginTbx.Text;
                    acc.Password = PasswordTbx.Password;

                    db.Accounts.Add(acc);

                    db.SaveChanges();
                    AccDgr.ItemsSource = db.Accounts.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!!"); }
            }
            catch { MessageBox.Show("Ошибка!"); }
            
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccDgr.SelectedItem != null)
                {
                    if (LoginTbx.Text.Length > 0 && PasswordTbx.Password.Length > 0 && PasswordTbx.Password.Length <= 25 && LoginTbx.Text.Length <= 25)
                    {
                        var strochka = AccDgr.SelectedItem as Accounts;
                        strochka.Login = LoginTbx.Text;
                        strochka.Password = PasswordTbx.Password;

                        db.SaveChanges();
                        AccDgr.ItemsSource = db.Accounts.ToList();
                    }
                    else { MessageBox.Show("Ошибка!"); }
                }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccDgr.SelectedItem != null)
                {
                    db.Accounts.Remove(AccDgr.SelectedItem as Accounts);

                    db.SaveChanges();
                    AccDgr.ItemsSource = db.Accounts.ToList();
                }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }
    }
}
