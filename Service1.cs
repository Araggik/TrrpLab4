using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace WcfServiceLibrary2
{
    //public struct Row
    //{
    //    public string data;
    //    public string loc;
    //    public int count;

    //    public Row(string data, string loc, int count)
    //    {
    //        this.data = data;
    //        this.loc = loc;
    //        this.count = count;
    //    }
    //}
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        
        public static string conn_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Towns;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static SqlConnection conn =new SqlConnection(conn_str);
        public static List<Row> rows = new List<Row>();
        public static int count_rows = 50;
        public static bool readdb = false;
        public static int piece = 5;

        public static void ReadDB()
        {
            conn.Open();
            var command = new SqlCommand(string.Format(@"SELECT TOP {0} * FROM People", count_rows), conn);
            var er = command.ExecuteReader();
            for (int i = 0; i < count_rows; i++)
            {
                er.Read();
                var newrow = new Row(er.GetDateTime(1).Year.ToString(), er.GetString(2), er.GetInt32(3));
                rows.Add(newrow);
            }
            er.Close();
            conn.Close();
        }

        public List<Row> GetRows(int index)
        {
            if(!readdb)
            {
                ReadDB();
                readdb = true;
            }
            var somerows = rows.GetRange(index * piece, piece);
            return somerows;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
