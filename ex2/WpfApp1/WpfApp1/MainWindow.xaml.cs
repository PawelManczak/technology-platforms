using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
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
            //tvi.MouseDoubleClick +=(s, e) => show_rahs(s, e, tvi);
            tvi.ContextMenu = new ContextMenu();

            var create = new MenuItem()
            {
                Header = "Create"
            };
            create.Click += (s, e) => createFile(s, e, tvi);
            var delete = new MenuItem()
            {
                Header = "Delete"
            };
            delete.Click += (s, e) => deleteDic(s, e, tvi);
            tvi.ContextMenu.Items.Add(create);
            tvi.ContextMenu.Items.Add(delete); 
        }

        private void show_rahs(object s, MouseButtonEventArgs e, TreeViewItem tvi)
        {
            var attr = File.GetAttributes(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
            //MessageBox.Show(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
            string[] c = { "-", "r", "a", "s", "h" };
            var item = tvi;
            var attrs = File.GetAttributes(Path.Combine((string)item.Tag, (string)item.Header));
            rash.Text =
                c[1 * (attr.HasFlag(FileAttributes.ReadOnly) ? 1 : 0)] +
                c[2 * (attr.HasFlag(FileAttributes.Archive) ? 1 : 0)] +
                c[3 * (attr.HasFlag(FileAttributes.System) ? 1 : 0)] +
                c[4 * (attr.HasFlag(FileAttributes.Hidden) ? 1 : 0)];
     
            //MessageBox.Show(buildstring);
        }

        private void setContextMenuFile(TreeViewItem tvi)
        {
            tvi.ContextMenu = new ContextMenu();
            tvi.MouseDoubleClick += (s, e) => show_rahs(s, e, tvi);
            var open= new MenuItem()
            {
                Header = "Open"
            };
            open.Click += (s, e) => openFile(s, e, tvi);
            var delete = new MenuItem()
            {
                Header = "Delete"
            };
            delete.Click += (s, e) => deleteFile(s, e, tvi);
            tvi.ContextMenu.Items.Add(open);
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
                if (File.Exists(System.IO.Path.Combine(tvi.Tag.ToString(), tvi.Header.ToString())))
                {
                    var path = tvi.Tag.ToString() + "\\" + tvi.Header.ToString();
                    var attr = File.GetAttributes(path);
                    if (attr.HasFlag(FileAttributes.ReadOnly))
                        File.SetAttributes(path, attr & ~FileAttributes.ReadOnly);

                    // If file found, delete it    
                    File.Delete(System.IO.Path.Combine(tvi.Tag.ToString(), tvi.Header.ToString()));
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

        private void createFile(object sender, EventArgs e, TreeViewItem tvi)
        {
            var cD = new CreateDialog(tvi);
            cD.ShowDialog();
            createATreeRoot(openedFolderPath);
        }
        private void openFile(object sender, EventArgs e, TreeViewItem tvi)
        {
            try
            {
                string text = System.IO.File.ReadAllText(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
                MessageBox.Show(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
                this.textBlock.Text = text;
            }
            catch (Exception ioExp)
            {
                this.textBlock.Text = "";
            }
            

            //rash
            var attr = File.GetAttributes(tvi.Tag.ToString() + "\\" + tvi.Header.ToString());
            var buildstring = "";
            if (attr.HasFlag(FileAttributes.ReadOnly)){
                buildstring += "r";
            }
            else buildstring += "-";

            if (attr.HasFlag(FileAttributes.Archive))
            {
                buildstring += "a";
            }
            else buildstring += "-";

            if (attr.HasFlag(FileAttributes.System))
            {
                buildstring += "s";
            }
            else buildstring += "-";

            if (attr.HasFlag(FileAttributes.Hidden))
            {
                buildstring += "h";
            }
            else buildstring += "-";

            rash.Text = buildstring;
             
        }

        public void toolbarExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


     

    }
}
