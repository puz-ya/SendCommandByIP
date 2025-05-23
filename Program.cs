using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SendCommandByIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || !IsValidIP(args[0]))
            {
                Console.WriteLine("Please provide a valid IP address. Port is optional (default: 9600).");
                return;
            }

            //Sleep for N sec to give FH/FHV7 some time to init
            System.Threading.Thread.Sleep(2000);

            //Case 1: SendCommandByIP 10.5.6.100
            //One time measure
            string ip = args[0];
            int port;
            int portUdp = 9600;    //default port for UDP for FH/FHV7
            int portTcp = 9876; //default port for TCP for FH/FHV7
            string command = "Measure";

            //Case 2: SendCommandByIP 10.5.6.100 9600
            //One time measure
            if (args.Length >= 2)
            {
                try
                {
                    port = Int32.Parse(args[1]);
                    portUdp = port;
                    portTcp = port;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Please provide a valid Port number. Unable to parse '{args[1]}'");
                    return;
                }

                if (port < 1 || port > 65535)
                {
                    Console.WriteLine("Please, provide a valid Port number.");
                    return;
                }
            }

            //Case 3: SendCommandByIP 10.5.6.100 9600 "Scene 3"
            //Any custom command
            if (args.Length == 3)
            {

                if (args[2].Length < 2 || args[2].Length > 32)
                {
                    Console.WriteLine("Please, provide a valid Command string (in double commas).");
                    return;
                }

                command = args[2];

            }

            //UDP part
            try
            {
                // Create a socket object.
                UdpClient udpClient = new UdpClient();

                // Set the destination IP and port.
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, portUdp);

                // Send the command "test" to the server.
                //string command = "test";
                byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                int bytesSent = udpClient.Send(data, data.Length, remoteEndPoint);

                /*
                // Receive a message from the remote host.
                IPEndPoint remoteHost = null;
                byte[] dataReply = udpClient.Receive(ref remoteHost);
                string response = Encoding.ASCII.GetString(dataReply);
                */

                Console.WriteLine("Command [" + command + "] sent to \n IP address: " + ip + " port " + portUdp.ToString());
                //Console.WriteLine("Reply:" + response);

                // Close the socket.
                udpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending the UDP command: " + ex.Message);
            }

            //TCP part
            try
            {
                // Create a TcpClient object.
                using (TcpClient client = new TcpClient())
                {
                    // Connect to the server at the specified IP address and port.
                    client.Connect(ip, portTcp);

                    // Get the network stream associated with the TcpClient object.
                    NetworkStream ns = client.GetStream();

                    // Send a command over TCP.
                    byte[] data = Encoding.ASCII.GetBytes(command);
                    Console.WriteLine("Sending...");
                    ns.Write(data, 0, data.Length);

                    // Receive a response from the server (if any).
                    int bytesReceived = ns.Read(data, 0, data.Length);
                    string response = Encoding.ASCII.GetString(data, 0, bytesReceived);
                    Console.WriteLine("Response: " + response);

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            //Console.ReadLine();
        }

        private static bool IsValidIP(string ipAddress)
        {
            try
            {
                IPAddress.Parse(ipAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

