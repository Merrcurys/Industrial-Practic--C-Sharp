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
    /// Логика взаимодействия для RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public RolePage()
        {
            InitializeComponent();

            RoleDgr.ItemsSource = db.Roles.ToList();
        }

        private void RoleDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleDgr.SelectedItem != null)
            {
                var strochka = RoleDgr.SelectedItem as Roles;
                RoleTbx.Text = strochka.Role;
            }
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                Roles role = new Roles();
                if (RoleTbx.Text.Length <= 50 && RoleTbx.Text.Length > 0)
                {
                    role.Role = RoleTbx.Text;

                    db.Roles.Add(role);

                    db.SaveChanges();
                    RoleDgr.ItemsSource = db.Roles.ToList();
                }
                else { MessageBox.Show("Длина роли больше допустимой"); }
            }
            catch { MessageBox.Show("Ошибка!"); }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (RoleDgr.SelectedItem != null)
            {
                try
                {
                    var strochka = RoleDgr.SelectedItem as Roles;
                    if (RoleTbx.Text.Length <= 50 && RoleTbx.Text.Length > 0)
                    {
                        strochka.Role = RoleTbx.Text;

                        db.SaveChanges();
                        RoleDgr.ItemsSource = db.Roles.ToList();
                    }
                    else { MessageBox.Show("Длина роли больше допустимой"); }
                }
                catch { MessageBox.Show("Ошибка!"); }
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoleDgr.SelectedItem != null)
                {
                    db.Roles.Remove(RoleDgr.SelectedItem as Roles);

                    db.SaveChanges();
                    RoleDgr.ItemsSource = db.Roles.ToList();
                }
            }
            catch { MessageBox.Show("Ошибка!"); }
        }
    }
}
