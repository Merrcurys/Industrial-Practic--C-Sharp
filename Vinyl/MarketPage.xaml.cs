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
    /// Логика взаимодействия для MarketPage.xaml
    /// </summary>
    public partial class MarketPage : Page
    {
        private VinylStoreEntities db = new VinylStoreEntities();
        decimal sum = 0;
        public MarketPage()
        {
            InitializeComponent();
            FilterCbx.ItemsSource = db.Condition.ToList();
            ProductsDgr.ItemsSource = db.Products.ToList();
            TotalLbl.Content = "Товары в чеке. Полная цена: " + sum.ToString();
        }
        List<Products> products = new List<Products>();

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckDgr.ItemsSource != null && products != null)
                {
                    decimal oplata;
                    if (decimal.TryParse(PriceTbx.Text, out oplata))
                    {
                        if (oplata > sum)
                        {
                            Info_Orders inf_ord = new Info_Orders();
                            inf_ord.Date = DateTime.Now;
                            inf_ord.Cost = sum;
                            inf_ord.Payment = Convert.ToDecimal(PriceTbx.Text);
                            inf_ord.Change = Convert.ToDecimal(PriceTbx.Text) - sum;
                            inf_ord.User_ID = 4;
                            db.Info_Orders.Add(inf_ord);
                            db.SaveChanges();

                            string txt = "Магазин виниловых пластинок\nКассовый чек №" + inf_ord.ID_Info_Orders;
                            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "check.txt");
                            File.WriteAllText(filePath, txt);

                            foreach (Products prod in products)
                            {
                                Info_Products inf_prd = new Info_Products();
                                inf_prd.Product_ID = prod.ID_Products;
                                inf_prd.Info_Orders_ID = inf_ord.ID_Info_Orders;
                                db.Info_Products.Add(inf_prd);
                                db.SaveChanges();

                                string prodInfo = "\n" + prod.Name_Album + " - " + prod.Price;
                                File.AppendAllText(filePath, prodInfo);
                            }
                            txt = "\nИтого к оплате: " + inf_ord.Cost + "\nВнесено: " + inf_ord.Payment + "\nСдача: " + inf_ord.Change;
                            File.AppendAllText(filePath, txt);
                            CheckDgr.ItemsSource = null;
                            sum = 0;
                            products = null;
                            TotalLbl.Content = "Товары в чеке. Полная цена: " + sum.ToString();
                        }
                        else { MessageBox.Show("Внесенных денег меньше чем в заказе"); }
                    }
                    else { MessageBox.Show("Не корректные данные!"); }
                }
                else { MessageBox.Show("Ошибка!"); }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void Button_Plus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsDgr.SelectedItem != null)
                {
                    var strochka = ProductsDgr.SelectedItem as Products;
                    products.Add(strochka);
                    sum += strochka.Price;
                    TotalLbl.Content = "Товары в чеке. Полная цена: " + sum.ToString();
                    CheckDgr.ItemsSource = null;
                    CheckDgr.ItemsSource = products;
                }
                else { MessageBox.Show("Выберите пластинку"); }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void Button_Minus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsDgr.SelectedItem != null)
                {
                    var strochka = ProductsDgr.SelectedItem as Products;
                    products.Remove(strochka);
                    sum -= strochka.Price;
                    TotalLbl.Content = "Товары в чеке. Полная цена: " + sum.ToString();
                    CheckDgr.ItemsSource = null;
                    CheckDgr.ItemsSource = products;
                }
                else { MessageBox.Show("Выберите пластинку"); }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            ProductsDgr.ItemsSource = db.Products.ToList().Where(item => item.Name_Album.Contains(SearchTxt.Text));
        }

        private void FilterCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (FilterCbx.SelectedItem != null)
                {
                    var selected = FilterCbx.SelectedItem as Condition;
                    ProductsDgr.ItemsSource = db.Products.ToList().Where(item => item.Condition == selected);
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            ProductsDgr.ItemsSource = db.Products.ToList();
        }
    }
}
