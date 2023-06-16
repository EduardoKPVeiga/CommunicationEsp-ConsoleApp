using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP
{
    public static class UDPClient
    {
        public static void SendData(IPAddress esp, int port, char keyPressed)
        {
            UdpClient udpClient = new UdpClient(port);
            try
            {
                udpClient.Connect(esp, port);
                // Sends a message to the host to which you have connected.
                Byte[] sendBytes = Encoding.ASCII.GetBytes(keyPressed + "\r");
                udpClient.Send(sendBytes, sendBytes.Length);
                udpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
