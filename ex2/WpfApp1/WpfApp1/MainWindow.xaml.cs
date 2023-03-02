using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

        }
        public void toolbarOpen_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = "Select directory to open" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    MessageBox.Show(path);


                    //create a tree
                    var root = new TreeViewItem
                    {
                        Header = Path.GetFileName(path),
                        Tag = Path.GetDirectoryName(path)
                    };

                    DirectoryInfo dir = new DirectoryInfo(path);
                    createATree(root, dir);

                    this.tree.Items.Add(root);
                }
            }

        }

        void createATree(TreeViewItem root, DirectoryInfo dir)
        {
            foreach (var item in dir.EnumerateFileSystemInfos())
            {
                TreeViewItem tvi = new TreeViewItem()
                {
                    Header = item.Name,
                    Tag = Path.GetDirectoryName(item.FullName)
                };

                if (item is DirectoryInfo)
                {
                    createATree(tvi, item as DirectoryInfo);
                }

                root.Items.Add(tvi);
            }
        }
        public void toolbarExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
