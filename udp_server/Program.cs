using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp_server
{
    internal class Program
    {
        const string address = "127.0.0.1"; // localhost: 127.0.0.1
        const ushort port = 3300;           // reccommended to use > 1024
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
            UdpClient server = new UdpClient(serverEndPoint);

            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                Console.WriteLine("Waiting for user request...");
                byte[] request = server.Receive(ref client); // waiting

                string message = Encoding.UTF8.GetString(request);
                Console.WriteLine("Got a message: " + message);

                string response = "It's a response for you!";
                byte[] data = Encoding.UTF8.GetBytes(response);
                server.Send(data, data.Length, client);
            }

            server.Close();
        }
    }
}