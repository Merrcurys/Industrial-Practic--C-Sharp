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
    /// Логика взаимодействия для MusiciansPage.xaml
    /// </summary>
    public partial class MusiciansPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public MusiciansPage()
        {
            InitializeComponent();
            MusDgr.ItemsSource = db.Musicians.ToList();
            TypeCbx.ItemsSource = db.Type.ToList();
        }

        private void MusDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MusDgr.SelectedItem != null)
            {
                var strochka = MusDgr.SelectedItem as Musicians;
                NameTbx.Text = strochka.Name;
                TypeCbx.SelectedItem = strochka.Type;
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
                Musicians musicat = new Musicians();
                if (Check(NameTbx.Text, 100))
                {
                    musicat.Name = NameTbx.Text;
                    musicat.Type_ID = (TypeCbx.SelectedItem as Type).ID_Type;

                    db.Musicians.Add(musicat);

                    db.SaveChanges();
                    MusDgr.ItemsSource = db.Musicians.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!"); }
            }
            catch { MessageBox.Show("ОШИБКА!"); }

        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MusDgr.SelectedItem != null && Check(NameTbx.Text, 100))
                {
                    var strochka = MusDgr.SelectedItem as Musicians;
                    strochka.Name = NameTbx.Text;
                    strochka.Type = (Type)TypeCbx.SelectedItem;

                    db.SaveChanges();
                    MusDgr.ItemsSource = db.Musicians.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!"); }
            }
            catch { MessageBox.Show("ОШИБКА!"); }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MusDgr.SelectedItem != null)
                {
                    db.Musicians.Remove(MusDgr.SelectedItem as Musicians);

                    db.SaveChanges();
                    MusDgr.ItemsSource = db.Musicians.ToList();
                }
            }
            catch { MessageBox.Show("ОШИБКА!"); }

        }
    }
}
