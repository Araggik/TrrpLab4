using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DataServer
{
    //struct Row
    //{
    //    public string data;
    //    public string loc;
    //    public int count;

    //    public Row(string data,string loc,int count)
    //    {
    //        this.data = data;
    //        this.loc = loc;
    //        this.count = count;
    //    }
    //}

    class DataServer
    {

        //public static string conn_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Towns;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static SqlConnection conn;
        //public static List<Row> rows;
        //public static int count_rows=50;
        public static string server_uri = "http://25.20.18.47:8201";

        //public static void ReadDB()
        //{
        //    var command = new SqlCommand(string.Format(@"SELECT TOP {0} * FROM People",count_rows), conn);
        //    var er = command.ExecuteReader();
        //    for(int i=0;i<count_rows;i++)
        //    {
        //        er.Read();
        //        var newrow = new Row(er.GetDateTime(1).Year.ToString(), er.GetString(2), er.GetInt32(3));
        //        rows.Add(newrow);
        //    }
        //    er.Close();
        //}

        static void Main(string[] args)
        {            
            //conn = new SqlConnection(conn_str);
            //conn.Open();
            //rows = new List<Row>();
            //ReadDB();

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,

                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };

            var host = new ServiceHost(typeof(WcfServiceLibrary2.Service1), new Uri(server_uri));
            host.Description.Behaviors.Add(behavior);
            host.AddServiceEndpoint(typeof(WcfServiceLibrary2.IService1), new BasicHttpBinding(), "basic");
            host.Open();

            Console.WriteLine("Running");
            Console.ReadKey();
            //conn.Close();
        }
    }
}
