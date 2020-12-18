using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperWebSocket;
using WebSocketSharp;

namespace Dispatcher
{
    class Program
    {
        public static int count_ports = 3;
        public static int count_server_ports = 3;
        public static int current_port = 0;
        public static WebSocketServer server;
        public static SortedList<string,WebSocketSession> list_servers;
        public static Dictionary<string, SortedList<string, string>> list_ports_serv;
        public static Dictionary<string, Stack<string>> list_free_ports;
        public static void MessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine(value);
            if(value =="Gserv")
            {                
                if (list_servers.Count > 0)
                {
                    var server = list_servers.First();
                    string server_adress = server.Key.Substring(1);
                    session.Send(server_adress);
                    var count_client = server.Key[0];
                    count_client++;
                    server_adress = count_client + server_adress;
                    list_servers.Add(server_adress, server.Value);
                    list_servers.Remove(server.Key);
                    Console.WriteLine("Connect c-s");
                }
                else
                {
                    session.Send("No");
                }                   
            } 
            else if(value.Substring(0,2)=="Gp")
            {
                string adress = value.Substring(2);
                if(list_ports_serv.ContainsKey(adress))
                {
                    var pair = list_ports_serv[adress].Last();
                    int port = int.Parse(pair.Key);
                    port++;
                    string newport = port.ToString();
                    list_ports_serv[adress].Add(newport, "");
                    session.Send(newport);
                    list_servers.Add("0" + adress + ":" + newport, session);
                }
                else
                {
                    string newport = "8101";
                    SortedList<string, string> newlist = new SortedList<string, string>();
                    newlist.Add(newport,"");
                    list_ports_serv.Add(adress, newlist);
                    session.Send(newport);
                    list_servers.Add("0" + adress + ":" + newport, session);
                }
                Console.WriteLine("AddServ");
            }
        }

        static void Main(string[] args)
        {
            for(int i=0;i<count_ports&&current_port==0;i++)
            {
                int port = 8001 + i;
                string adress = "ws://25.20.18.47:" + port.ToString();
                var client = new WebSocket(adress);
                client.Connect();
                Console.Clear();
                if (!client.IsAlive)
                {                  
                    current_port = port;
                }
                client.Close();
            }
            if(current_port!=0)
            {
                server = new WebSocketServer();
                server.Setup(current_port);
                server.NewMessageReceived += MessageReceived;
                server.Start();

                list_servers = new SortedList<string, WebSocketSession>();
                list_ports_serv = new Dictionary<string, SortedList<string, string>>();
                list_free_ports = new Dictionary<string, Stack<string>>();

                Console.WriteLine("Ready");
                Console.ReadKey();
                server.Stop();
                
            }
            Console.ReadKey();
        }
    }
}
