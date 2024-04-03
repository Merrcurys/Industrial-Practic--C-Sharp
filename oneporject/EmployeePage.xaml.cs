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
        private FactoryEntities db = new FactoryEntities(); // FactoryEntities имя бд
        public EmployeePage()
        {
            InitializeComponent();

            employeDgr.ItemsSource = db.Employees.ToList(); // чтение и отображение бд в датагрид
            DirectorCbx.ItemsSource = db.Directors.ToList();
            FilterCbx.ItemsSource = db.Directors.ToList();
        }

        private void employeDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeDgr.SelectedItem != null)
            {
                var strochka = employeDgr.SelectedItem as Employees;
                SurnameTbx.Text = strochka.Surname_Employee;
                PatrTbx.Text = strochka.Patronymic_Employee;
                NameTbx.Text = strochka.Name_Employee;
                DirectorCbx.SelectedItem = strochka.Directors;
            }
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            Employees empl = new Employees();
            empl.Surname_Employee = SurnameTbx.Text;
            empl.Patronymic_Employee = PatrTbx.Text;
            empl.Name_Employee = NameTbx.Text;
            empl.Director_ID = (DirectorCbx.SelectedItem as Directors).ID_Director;

            db.Employees.Add(empl);

            db.SaveChanges();
            employeDgr.ItemsSource = db.Employees.ToList();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (employeDgr.SelectedItem != null)
            {
                db.Employees.Remove(employeDgr.SelectedItem as Employees);

                db.SaveChanges();
                employeDgr.ItemsSource = db.Employees.ToList();
            }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (employeDgr.SelectedItem != null)
            {
                var strochka = employeDgr.SelectedItem as Employees;
                strochka.Surname_Employee = SurnameTbx.Text;
                strochka.Patronymic_Employee = PatrTbx.Text;
                strochka.Name_Employee = NameTbx.Text;
                strochka.Directors = (Directors)DirectorCbx.SelectedItem;

                db.SaveChanges();
                employeDgr.ItemsSource = db.Employees.ToList();
            }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            employeDgr.ItemsSource = db.Employees.ToList().Where(item => item.Surname_Employee.Contains(SearchTxt.Text));
        }

        private void FilterCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterCbx.SelectedItem != null)
            {
                var selected = FilterCbx.SelectedItem as Directors;
                employeDgr.ItemsSource = db.Employees.ToList().Where(item => item.Directors == selected);
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            employeDgr.ItemsSource = db.Employees.ToList();
        }
    }
}
