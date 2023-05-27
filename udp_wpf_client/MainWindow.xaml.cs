using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace udp_wpf_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPEndPoint? serverEdnpoint = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SendBtnClick(object sender, RoutedEventArgs e)
        {
            serverEdnpoint = new IPEndPoint(IPAddress.Parse(addressTxtBox.Text), int.Parse(portTxtBox.Text));

            using UdpClient client = new();

            string message = msgTxtBox.Text;

            // TODO: add validations

            byte[] data = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(data, data.Length, serverEdnpoint);

            historyList.Items.Add($"Me: {message} at {DateTime.Now.ToShortTimeString()}");

            var response = await client.ReceiveAsync();

            string responseText = Encoding.UTF8.GetString(response.Buffer);

            historyList.Items.Add($"Server: {responseText} [{response.RemoteEndPoint}] at {DateTime.Now.ToShortTimeString()}");
        }
    }
}
