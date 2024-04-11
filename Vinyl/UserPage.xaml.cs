using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vinyl
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public UserPage()
        {
            InitializeComponent();
            UserDgr.ItemsSource = db.Users.ToList();
            RoleCbx.ItemsSource = db.Roles.ToList();
            AccCbx.ItemsSource = db.Accounts.ToList();
        }

        private void UserDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDgr.SelectedItem != null)
            {
                var strochka = UserDgr.SelectedItem as Users;
                SurmameTbx.Text = strochka.Surname;
                PatrnomycTbx.Text = strochka.Patronymic;
                NameTbx.Text = strochka.Name;
                AccCbx.SelectedItem = strochka.Accounts;
                RoleCbx.SelectedItem = strochka.Roles;
            }
        }

        private bool Check(string text, int num)
        {
            if (text.Length > 0 && text.Length <= num) { return true; }
            return false;
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                Users user = new Users();
                if (Check(SurmameTbx.Text, 100) && Check(PatrnomycTbx.Text, 100) && Check(NameTbx.Text, 100))
                {
                    user.Surname = SurmameTbx.Text;
                    user.Patronymic = PatrnomycTbx.Text;
                    user.Name = NameTbx.Text;
                    user.Account_ID = (AccCbx.SelectedItem as Accounts).ID_Account;
                    user.Role_ID = (RoleCbx.SelectedItem as Roles).ID_Role;

                    db.Users.Add(user);

                    db.SaveChanges();
                    UserDgr.ItemsSource = db.Users.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!"); }

            }
            catch { MessageBox.Show("Ошибка!"); }

        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserDgr.SelectedItem != null)
                {
                    var strochka = UserDgr.SelectedItem as Users;
                    if (Check(SurmameTbx.Text, 100) && Check(PatrnomycTbx.Text, 100) && Check(NameTbx.Text, 100))
                    {
                        strochka.Surname = SurmameTbx.Text;
                        strochka.Patronymic = PatrnomycTbx.Text;
                        strochka.Name = NameTbx.Text;
                        strochka.Accounts = (Accounts)AccCbx.SelectedItem;
                        strochka.Roles = (Roles)RoleCbx.SelectedItem;

                        db.SaveChanges();
                        UserDgr.ItemsSource = db.Users.ToList();
                    }
                    else { MessageBox.Show("Ошибка валидации!"); }
                    
                }
                else { MessageBox.Show("Не выбран элемент!"); }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserDgr.SelectedItem != null)
                {
                    db.Users.Remove(UserDgr.SelectedItem as Users);

                    db.SaveChanges();
                    UserDgr.ItemsSource = db.Users.ToList();
                }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }
    }
}
