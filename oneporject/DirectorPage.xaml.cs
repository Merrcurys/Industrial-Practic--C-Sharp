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

            directorDgr.ItemsSource = employee.GetFullInfo();
            FilterCbx.ItemsSource = director.GetData();
            FilterCbx.DisplayMemberPath = "Name_Director";
        }

        private void directorDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (directorDgr.SelectedItem != null)
            {
                var strochka = (directorDgr.SelectedItem as DataRowView).Row;
                SurnTbx.Text = strochka[5].ToString();
                PatrTbx.Text = strochka[6].ToString();
                NameTbx.Text = strochka[7].ToString();
            }
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            if (directorDgr.SelectedItem != null)
            {
                director.InsertQuery(SurnTbx.Text, NameTbx.Text, PatrTbx.Text);
                directorDgr.ItemsSource = employee.GetFullInfo();
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (directorDgr.SelectedItem != null)
            {
                object id = (directorDgr.SelectedItem as DataRowView).Row[0];
                director.DeleteQuery(Convert.ToInt32(id));
                directorDgr.ItemsSource = employee.GetFullInfo();
            }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (directorDgr.SelectedItem != null)
            {
                object id = (directorDgr.SelectedItem as DataRowView).Row[4];
                director.UpdateQuery(SurnTbx.Text, NameTbx.Text, PatrTbx.Text, Convert.ToInt32(id));
                directorDgr.ItemsSource = employee.GetFullInfo();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            directorDgr.Columns[0].Visibility = Visibility.Collapsed;
            directorDgr.Columns[4].Visibility = Visibility.Collapsed;
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            directorDgr.ItemsSource = employee.SearchBySurname(SearchTxt.Text);
        }

        private void FilterCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterCbx.SelectedItem != null)
            {
                var id = (int)(FilterCbx.SelectedItem as DataRowView).Row[0];
                directorDgr.ItemsSource = employee.FilterByDirector(id);
            }
        }

        private void Button_Filter(object sender, RoutedEventArgs e)
        {
            directorDgr.ItemsSource = employee.GetFullInfo();
            directorDgr.Columns[0].Visibility = Visibility.Collapsed;
            directorDgr.Columns[4].Visibility = Visibility.Collapsed;
        }
    }
}
