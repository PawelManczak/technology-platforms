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
        private string openedFolderPath = "";

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
                    //MessageBox.Show(path);
                    openedFolderPath = path;

                    //create a tree
                    createATreeRoot(path);
                }
            }

        }
        void createATreeRoot(string path)
        {
            tree.Items.Clear();
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
            delete.Click += (s, e) => deleteDic(s, e, tvi);
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
            delete.Click += (s, e) => deleteFile(s, e, tvi);
            tvi.ContextMenu.Items.Add(create);
            tvi.ContextMenu.Items.Add(delete);
        }

        private void deleteDic(object sender, EventArgs e, TreeViewItem tvi)
        {
            MessageBox.Show(tvi.Tag.ToString() + tvi.Header.ToString());
            Directory.Delete(tvi.Tag.ToString()+ "\\" + tvi.Header.ToString(), true);
            createATreeRoot(openedFolderPath);
        }
        private void deleteFile(object sender, EventArgs e, TreeViewItem tvi)
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(tvi.Tag.ToString(), tvi.Header.ToString())))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(tvi.Tag.ToString(), tvi.Header.ToString()));
                    Console.WriteLine("File deleted.");
                    createATreeRoot(openedFolderPath);
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
       
        }
        private void openFile(object sender, EventArgs e, TreeViewItem tvi)
        {
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
