using System.Windows;
using System.Windows.Forms;
using WpfApp1.treeView;
using MessageBox = System.Windows.MessageBox;
using TreeView = System.Windows.Forms.TreeView;
using Window = System.Windows.Window;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

        }
        private static void PopulateTreeView(TreeView treeView, string[] paths, char pathSeparator)
        {
            //Creates a tree from given path list
            foreach (string path in paths)
            {
                TreeNodeCollection nodes = treeView.Nodes;

                foreach (string path_part in path.Split('\\'))
                {
                    //Here it adds a new node (file or folder)
                    if (!nodes.ContainsKey(path_part))
                        nodes.Add(path_part, path_part);
                    //Go one node deeper
                    nodes = nodes[path_part].Nodes;
                }
            }
        }
        public void toolbarOpen_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = "Select directory to open" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    MessageBox.Show(path);
                    string[] paths = new string[1];
                    paths[0] = path;
                    var itemProvider = new ItemProvider();

                    var items = itemProvider.GetItems(path);

                    DataContext = items;
                    //create a tree
                    // PopulateTreeView(this.myTreeViewEvent, paths, '/');
                }
            }

        }

        public void toolbarExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
