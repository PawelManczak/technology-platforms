using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace PlatformyLab6
{
    public partial class MainWindow : Window
    {

        private int mouseXDown, mouseYDown;
        public MainWindow()
        {
            InitializeComponent();

            Thread th = new Thread(() => { StartServer(); }); 
            th.IsBackground = true; 
            th.Start();
   
        }

        public void StartServer()
        {
            UdpClient udpServer = new UdpClient(1234); // port number
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
            while (true)
            {
                byte[] receiveBytes = udpServer.Receive(ref remoteEP);
                string receivedMessage = Encoding.ASCII.GetString(receiveBytes);

                // decode received message to get int values
                var intValues = JsonConvert.DeserializeObject<int[]>(receivedMessage);

                // process received int values here
                int x1 = intValues[0];
                int y1 = intValues[1];
                int x2 = intValues[2];
                int y2 = intValues[3];
                Send(x1, y1, x2, y2);   

                Dispatcher.Invoke(() => create_new_line(x1, y1, x2, y2));

                //udpServer.Send(receiveBytes, receiveBytes.Length, remoteEP);
            }
        }

        private void Send(int x1, int y1, int x2, int y2)
        {
            UdpClient udpClient = new UdpClient();
            int[] values = new int[] { x1, y1, x2, y2 };
            string message = JsonConvert.SerializeObject(values);
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4321); // server IP address and port number
            udpClient.Send(sendBytes, sendBytes.Length, remoteEP);
            Console.WriteLine("server is sending!");
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
