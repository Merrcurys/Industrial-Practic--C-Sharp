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
    /// Логика взаимодействия для Cheki.xaml
    /// </summary>
    public partial class Cheki : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        public Cheki()
        {
            InitializeComponent();
            cheqCbx.ItemsSource = db.Info_Orders.ToList();
        }

        private void cheqCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cheqCbx.SelectedItem != null)
                {
                    var selected = cheqCbx.SelectedItem as Info_Orders;
                    List<Products> products = new List<Products>();
                    foreach (var item in db.Info_Products.ToList().Where(item => item.Info_Orders_ID == selected.ID_Info_Orders))
                    {
                        foreach (Products prd in db.Products.ToList())
                        {
                            if (prd.ID_Products == item.Product_ID)
                            {
                                products.Add(prd);
                            }
                        }
                    }
                    cheqDgr.ItemsSource = products.ToList();
                    foreach (var item in db.Users.ToList().Where(item => item.ID_User == selected.User_ID))
                    {
                        foreach (Users user in db.Users.ToList())
                        {
                            if (user.ID_User == selected.User_ID)
                            {
                                emplTbx.Text = user.Name + " " + user.Patronymic + " " + user.Surname;
                            }
                        }
                    }
                    date.Text = selected.Date.ToString();
                    total.Text = selected.Cost.ToString();
                }
            }
            catch { MessageBox.Show("Ошибка!"); }
        }

        private void Button_Unload(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Products> products = (List<Products>)cheqDgr.ItemsSource;
                var inf_ord = cheqCbx.SelectedItem as Info_Orders;
                string txt = "Книжный\nКассовый чек №" + inf_ord.ID_Info_Orders;
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "check.txt");
                File.WriteAllText(filePath, txt);
                foreach (Products product in products)
                {
                    string prodInfo = "\n" + product.Name_Album + " - " + product.Price;
                    File.AppendAllText(filePath, prodInfo);
                }
                txt = "\nИтого к оплате: " + inf_ord.Cost + "\nВнесено: " + inf_ord.Payment + "\nСдача: " + inf_ord.Change;
                File.AppendAllText(filePath, txt);
            }
            catch { MessageBox.Show("Ошибка!"); }

        }
    }
}
