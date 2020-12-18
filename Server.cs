using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        public static int count_ports = 3;
        public static int current_port = 0;
        public static string current_adress = "";
        public static WebSocket dis_client;
        public static WebSocket dat_client;
        public static void OnMes(object sender, MessageEventArgs e)
        {
            if (e.Data.Length == 4)
            {
                current_port = int.Parse(e.Data);
                Console.WriteLine(current_port);
            }
        }
        static void Main(string[] args)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var adress_list = host.AddressList;
            var len = adress_list.Length;
            for (int i = 0; i < len && current_adress == ""; i++)
            {
                var ip = adress_list[i];
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().Substring(0, 3) != "192")
                {
                    current_adress = ip.ToString();
                }
            }
            bool notfind = true;
            for (int i = 0; i < count_ports && notfind; i++)
            {
                int port = 8001 + i;
                string adress = "ws://25.20.18.47:" + port.ToString();
                dis_client = new WebSocket(adress);
                dis_client.Connect();               
                Console.Clear();
                Console.WriteLine(i);
                if (dis_client.IsAlive)
                {
                    notfind = false;
                    dis_client.OnMessage += OnMes;
                    dis_client.Send("Gp" + current_adress);                    
                }
                else
                {
                    dis_client.Close();
                }
            }                     
            Console.WriteLine(current_adress);
            Console.ReadKey();
        }
    }
}
