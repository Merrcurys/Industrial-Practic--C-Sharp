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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VinylStoreEntities db = new VinylStoreEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var account = db.Accounts.Where((acc) => acc.Login == LoginTbx.Text && acc.Password == PasswordTbx.Password).Single();

                if (account != null)
                {
                    var user = db.Users.Where((us) => us.Account_ID == account.ID_Account).Single();
                    if (user != null)
                    {
                        int roleId = user.Role_ID;

                        switch (roleId)
                        {
                            case 1:
                                Admin adminWindow = new Admin();
                                adminWindow.Show();
                                Close();
                                break;
                            case 2:
                                Sclad scladWindow = new Sclad();
                                scladWindow.Show();
                                Close();
                                break;
                            case 3:
                                Cashier cashierWindow = new Cashier();
                                cashierWindow.Show();
                                Close();
                                break;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Такого пользователя нет!"); } 
        }
    }
}
