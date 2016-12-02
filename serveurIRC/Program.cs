using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace serveurIRC
{
    class Program
    {
        static void Main(string[] args)
        {
            string IRCtalk = @"c:\";
            string fileName = "IRACID.log";
            string pathString = System.IO.Path.Combine(IRCtalk, "IRACID");
            pathString = System.IO.Path.Combine(pathString, fileName);

             try
            {
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 8001);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                byte[] b = new byte[100];
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");
                char[] msg;
                if (!System.IO.File.Exists(pathString))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                    {
                        for (int i = 0; i < k; i++)
                            Console.WriteLine(Convert.ToChar(b[i]));
                        // msg[i] = Convert.ToChar(b[i]);//affichage bitabit
                    }
                }
                else
                {
                    Console.WriteLine("File \"{0}\" already exists.", fileName);
                }


                ASCIIEncoding asen = new ASCIIEncoding();   //creation de la rep
                s.Send(asen.GetBytes("The string was recieved by the server.")); // ce qu'il envoie pour confirmer
                Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

    }
}