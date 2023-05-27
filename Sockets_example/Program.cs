using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets_example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Server Endpoint: address + port
            const string address = "127.0.0.1"; // localhost: 127.0.0.1
            const ushort port = 3300; // reccommended to use > 1024
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(address), port);

            UdpClient client = new UdpClient();

            Console.Write("Enter a message: ");
            string message = Console.ReadLine();

            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, serverEndPoint);

            byte[] response = client.Receive(ref serverEndPoint);
            Console.WriteLine("Server response: " + Encoding.UTF8.GetString(response));

            client.Close();
        }
    }
}