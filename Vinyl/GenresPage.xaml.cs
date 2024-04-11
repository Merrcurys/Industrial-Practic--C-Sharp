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
    /// Логика взаимодействия для GenresPage.xaml
    /// </summary>
    public partial class GenresPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public GenresPage()
        {
            InitializeComponent();

            GenreDgr.ItemsSource = db.Genres.ToList();
        }

        private void GenreDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenreDgr.SelectedItem != null)
            {
                var strochka = GenreDgr.SelectedItem as Genres;
                GenreTbx.Text = strochka.Genre;
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
                Genres genr = new Genres();
                if (Check(GenreTbx.Text, 100))
                {
                    genr.Genre = GenreTbx.Text;

                    db.Genres.Add(genr);

                    db.SaveChanges();
                    GenreDgr.ItemsSource = db.Genres.ToList();
                }
                else { MessageBox.Show("ошибка!"); }
            }
            catch { MessageBox.Show("ошибка!"); }

        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GenreDgr.SelectedItem != null && Check(GenreTbx.Text, 100))
                {
                    var strochka = GenreDgr.SelectedItem as Genres;
                    strochka.Genre = GenreTbx.Text;

                    db.SaveChanges();
                    GenreDgr.ItemsSource = db.Genres.ToList();
                }
                else { MessageBox.Show("ошибка!"); }
            }
            catch { MessageBox.Show("ошибка!"); }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GenreDgr.SelectedItem != null)
                {
                    db.Genres.Remove(GenreDgr.SelectedItem as Genres);

                    db.SaveChanges();
                    GenreDgr.ItemsSource = db.Genres.ToList();
                }
            }
            catch { MessageBox.Show("ошибка!"); }

        }

        private void Button_Import(object sender, RoutedEventArgs e)
        {
            try
            {
                List<genreModel> forImport = JsonConvertor.DeserializeObject<List<genreModel>>();
                foreach (var item in forImport)
                {
                    Genres genr = new Genres();
                    genr.Genre = item.Genre;

                    db.Genres.Add(genr);

                    db.SaveChanges();
                }
                GenreDgr.ItemsSource = null;
                GenreDgr.ItemsSource = db.Genres.ToList();
            }
            catch { MessageBox.Show("ошибка импорта!"); }

        }
    }
}
