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
using System.Data;
using oneporject.FactoryTableAdapters;

namespace oneporject
{
    /// <summary>
    /// Логика взаимодействия для DirectorPage.xaml
    /// </summary>
    public partial class DirectorPage : Page
    {
        // DataSet
        EmployeesTableAdapter employee = new EmployeesTableAdapter();
        DirectorsTableAdapter director = new DirectorsTableAdapter();
        public DirectorPage()
        {
            InitializeComponent();

            directorDgr.ItemsSource = director.GetData();
        }

        private void directorDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var strochka = (directorDgr.SelectedItem as DataRowView).Row;
            NameTbx.Text = strochka[2].ToString();
        }
    }
}
