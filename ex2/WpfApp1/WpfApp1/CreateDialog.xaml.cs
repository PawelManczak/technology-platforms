using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy CreateDialog.xaml
    /// </summary>
    public partial class CreateDialog : Window
    {
        string name;
        bool file, directory, archive, hidden, system, readOnly;
        TreeViewItem mTvi;
        public CreateDialog(TreeViewItem tvi)
        {
            mTvi = tvi;
            InitializeComponent();
        }
        public void ButtonOk(object sender, RoutedEventArgs e)
        {

            if (FileR.IsChecked == true) 
                file = true;
            else file = false;  

            if(DirectoryR.IsChecked == true)
                directory = true;
            else directory = false;
            name = textInput.Text;

            if (Regex.Match(name, @"^[A-Za-z0-9_~\-]{1,8}\.(txt|php|html)$").Success == false  && file)
            {
                MessageBox.Show("Zła nazwa!");
                return;
            }
            archive = Archive.IsChecked == true;
            hidden = Hidden.IsChecked == true;  
            system = SystemC.IsChecked == true;

            var file_path = mTvi.Tag.ToString() + "\\" + mTvi.Header.ToString() + "\\" + name;
            // MessageBox.Show(file_path);
            if (file) {
                File.Create(file_path);
                FileAttributes attr = 0;
                //atributes
                if (archive)
                    attr |= FileAttributes.Archive;
                if (readOnly)
                    attr |= FileAttributes.ReadOnly;
                if (hidden)
                    attr |= FileAttributes.Hidden;
                if (system)
                    attr |= FileAttributes.System;

                File.SetAttributes(file_path, attr);
            }
            else Directory.CreateDirectory(file_path);

            

            

            
            this.Close();
        }

        public void ButtonCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
