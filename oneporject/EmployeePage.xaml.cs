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

namespace oneporject
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        // EntityFramework
        FactoryEntities db = new FactoryEntities(); // FactoryEntities
        public EmployeePage()
        {
            InitializeComponent();

            employeDgr.ItemsSource = db.Employees.ToList();
            DirectorCbx.ItemsSource = db.Directors.ToList();
        }

        private void employeDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var strochka = employeDgr.SelectedItem as Employees;
            NameTbx.Text = strochka.Surname_Employee;
            DirectorCbx.SelectedItem = strochka.Directors;
        }
    }
}
