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
using System.Windows.Shapes;

namespace Vinyl
{
    /// <summary>
    /// Логика взаимодействия для Sclad.xaml
    /// </summary>
    public partial class Sclad : Window
    {
        public Sclad()
        {
            InitializeComponent();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ProductsPage();
        }

        private void Genres_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new GenresPage();
        }

        private void Musicians_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new MusiciansPage();
        }

        private void Other_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new OtherPage();
        }
    }
}
