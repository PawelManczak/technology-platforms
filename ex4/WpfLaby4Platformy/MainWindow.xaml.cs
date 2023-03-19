using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;

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
            //OutputWriter.RemoveOutputFile();
            //DataController.LinqStatements();
            //DataController.PerformOperations();
            //DataController.SortingAndSearchingWithCarBindingList();


            InitializeComponent();
            //InitComboBox();
            ClearSortingTable();
            myCarsBindingList = new CarBindingList(DataHandler.myCars);
            carBindingSource = new BindingSource();
            UpdateDataGrid();

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
    }

    
}
