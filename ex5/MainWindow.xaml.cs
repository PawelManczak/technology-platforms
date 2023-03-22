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

namespace PlatformyLab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int mouseXDown, mouseYDown, mouseXUp, mouseYUp;
        public MainWindow()
        {
            InitializeComponent();

            create_new_line(10, 10, 30, 30);


        }

 

        private void onCanvasMouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mousedown");
            Console.WriteLine(e.GetPosition(this));

            mouseXDown = (int) e.GetPosition(this).X;
            mouseYDown = (int) e.GetPosition(this).Y;
            
        }
        private void onCanvasMouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse up");
            Console.WriteLine(e.GetPosition(this));

            create_new_line((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y, mouseXDown, mouseYDown);

        }

        void create_new_line(int x1, int y1, int x2, int y2)
        {
            Brush brush = Brushes.Red;
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = brush,
                StrokeThickness = 5
            };
            canvas.Children.Add(line);
        }
    }
}
