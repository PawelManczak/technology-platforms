using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace WpfLaby4Platformy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        
        private Dictionary<string, bool> sorting = new Dictionary<string, bool>();

        private CarBindingList myCarsBindingList;
        private BindingSource carBindingSource;
        
        public MainWindow()
        {
            DataHandler.exercise1();
            DataHandler.exercise2();


            InitializeComponent();
            setComboBox();
            ClearSortingTable();
            myCarsBindingList = new CarBindingList(DataHandler.myCars);
            carBindingSource = new BindingSource();
            UpdateDataGrid();

        }

        private void ButtonSearch(object sender, RoutedEventArgs e)
        {
            CheckForNewItems();
            myCarsBindingList = new CarBindingList(DataHandler.myCars);
            List<Car> resultListOfCars;
            Int32 tmp;
            if (!searchTextBox.Text.Equals(""))
            {
                string property = comboBox.SelectedItem.ToString();
                if (Int32.TryParse(searchTextBox.Text, out tmp))
                {
                    resultListOfCars = myCarsBindingList.FindCars(property, tmp);
                }
                else
                {
                    resultListOfCars = myCarsBindingList.FindCars(property, searchTextBox.Text);
                }

                myCarsBindingList = new CarBindingList(resultListOfCars);
                UpdateDataGrid();
            }
        }
        private void UpdateDataGrid()
        {
            carBindingSource.DataSource = myCarsBindingList;
            dataGridView1.ItemsSource = carBindingSource;
        }

        private void ButtonDeleteRow(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    Car car = (Car)row.Item;
                    myCarsBindingList.Remove(car);
                    DataHandler.myCars.Remove(car);
                    UpdateDataGrid();
                    break;
                }
            }
        }

        private void SortColumn(object sender, RoutedEventArgs e)
        {
            var columnHeader = sender as DataGridColumnHeader;
            string columnName = columnHeader.ToString().Split(' ')[1].ToLower();
            bool isAsc = sorting[columnName];
            ClearSortingTable();
            if (isAsc == true)
            {
                myCarsBindingList.Sort(columnName, ListSortDirection.Descending);
            }
            else
            {
                myCarsBindingList.Sort(columnName, ListSortDirection.Ascending);
            }
            sorting[columnName] = !isAsc;
            UpdateDataGrid();
        }
        private void ClearSortingTable()
        {
            sorting.Clear();
            sorting.Add("model", false);
            sorting.Add("motor", false);
            sorting.Add("year", false);

        }

        private void ButtonReload(object sender, RoutedEventArgs e)
        {
            myCarsBindingList = new CarBindingList(DataHandler.myCars);
            UpdateDataGrid();
        }

        private void setComboBox()
        {
            BindingList<string> list = new BindingList<string>();
            list.Add("model");
            list.Add("year");
            list.Add("motor.displacement");
            list.Add("motor.model");
            list.Add("motor.horsePower");
            comboBox.ItemsSource = list;
            comboBox.SelectedIndex = 0;
        }

        private void CheckForNewItems()
        {
            foreach (Car item in myCarsBindingList)
            {
                if (!DataHandler.myCars.Contains(item))
                {
                    DataHandler.myCars.Add(item);
                }
            }
        }
    }

    
}
