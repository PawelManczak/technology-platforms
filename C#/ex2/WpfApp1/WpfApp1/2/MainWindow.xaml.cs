using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
                    setContextMenuDir(root);
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
                    setContextMenuDir(tvi);

                }
                else
                {
                    setContextMenuFile(tvi);
                }
                root.Items.Add(tvi);
                

            }
        }

        private void setContextMenuDir(TreeViewItem tvi)
        {
            tvi.ContextMenu = new ContextMenu();

            var create = new MenuItem()
            {
                Header = "Create"
            };

            var delete = new MenuItem()
            {
                Header = "Delete"
            };
            tvi.ContextMenu.Items.Add(create);
            tvi.ContextMenu.Items.Add(delete); 
        }

        private void setContextMenuFile(TreeViewItem tvi)
        {
            tvi.ContextMenu = new ContextMenu();

            var create = new MenuItem()
            {
                Header = "Open"
            };
            create.Click += (s, e) => openFile(s, e, tvi);
            var delete = new MenuItem()
            {
                Header = "Delete"
            };
            tvi.ContextMenu.Items.Add(create);
            tvi.ContextMenu.Items.Add(delete);
        }

 

        private void openFile(object sender, EventArgs e, TreeViewItem tvi)
        {
            FileInfo fi = new FileInfo(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
            string text = System.IO.File.ReadAllText(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
            MessageBox.Show(tvi.Tag.ToString() + "\\" + tvi.Header.ToString() );
            this.textBlock.Text = text;

        }

        public void toolbarExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


     

    }
}
