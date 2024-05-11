using System;
using Speaker.leison.Sistem.layer.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Speaker.leison.Sistem.layer.Repositories
{
    public class UdpComunicationRepository: IUdpComunicationRepository
    {
        public UdpComunicationRepository()
        {
                
        }
        public string Receive()
        {
            int port = 12345;

            UdpClient server = new UdpClient(port);

            Console.WriteLine("UDP Server is listening on port " + port);

            try
            {
                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = server.Receive(ref clientEndPoint);

                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);
                    return receivedMessage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                server.Close();
            }

            return "";
        }
    }
}
