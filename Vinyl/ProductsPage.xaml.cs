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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public ProductsPage()
        {
            InitializeComponent();
            ProductsDgr.ItemsSource = db.Products.ToList();
            CoverCbx.ItemsSource = db.Condition.ToList();
            VinylCbx.ItemsSource = db.Condition.ToList();
            MusCbx.ItemsSource = db.Musicians.ToList();
            GenreCbx.ItemsSource = db.Genres.ToList();
        }
        private void ProductsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                var strochka = ProductsDgr.SelectedItem as Products;
                AlbumTbx.Text = strochka.Name_Album;
                PriceTbx.Text = Convert.ToString(strochka.Price);
                CoverCbx.SelectedItem = strochka.Condition;
                VinylCbx.SelectedItem = strochka.Condition1;
                MusCbx.SelectedItem = strochka.Musicians;
                GenreCbx.SelectedItem = strochka.Genres;
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
                Products prod = new Products();
                if (Check(AlbumTbx.Text, 100))
                {
                    prod.Name_Album = AlbumTbx.Text;
                    prod.Price = Convert.ToDecimal(PriceTbx.Text);
                    prod.Condition_ID_Cover = (CoverCbx.SelectedItem as Condition).ID_Condition;
                    prod.Condition_ID_Vinyl = (VinylCbx.SelectedItem as Condition).ID_Condition;
                    prod.Musician_ID = (MusCbx.SelectedItem as Musicians).ID_Musician;
                    prod.Genre_ID = (GenreCbx.SelectedItem as Genres).ID_Genre;

                    db.Products.Add(prod);

                    db.SaveChanges();
                    ProductsDgr.ItemsSource = db.Products.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!"); }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsDgr.SelectedItem != null && Check(AlbumTbx.Text, 100))
                {
                    var strochka = ProductsDgr.SelectedItem as Products;
                    strochka.Name_Album = AlbumTbx.Text;
                    strochka.Price = Convert.ToDecimal(PriceTbx.Text);
                    strochka.Condition = (Condition)CoverCbx.SelectedItem;
                    strochka.Condition1 = (Condition)VinylCbx.SelectedItem;
                    strochka.Genres = (Genres)GenreCbx.SelectedItem;

                    db.SaveChanges();
                    ProductsDgr.ItemsSource = db.Products.ToList();
                }
                else { MessageBox.Show("Ошибка валидации!"); }
            }
            catch { MessageBox.Show("Ошибка!"); }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                db.Products.Remove(ProductsDgr.SelectedItem as Products);

                db.SaveChanges();
                ProductsDgr.ItemsSource = db.Products.ToList();
            }
        }

        private void Button_Import(object sender, RoutedEventArgs e)
        {
            try
            {
                List<productModel> forImport = JsonConvertor.DeserializeObject<List<productModel>>();
                foreach (var item in forImport)
                {
                    Products prod = new Products();
                    prod.Name_Album = item.Name_Album;
                    prod.Price = item.Price;
                    prod.Condition_ID_Cover = item.Condition_ID_Cover;
                    prod.Condition_ID_Vinyl = item.Condition_ID_Vinyl;
                    prod.Musician_ID = item.Musician_ID;
                    prod.Genre_ID = item.Genre_ID;

                    db.Products.Add(prod);

                    db.SaveChanges();
                }
                ProductsDgr.ItemsSource = null;
                ProductsDgr.ItemsSource = db.Products.ToList();
            }
            catch { MessageBox.Show("Ошибка импорта"); }

        }
    }
}
