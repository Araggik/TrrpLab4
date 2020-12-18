using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataServer
{
    class Program
    {       
        static void Main(string[] args)
        {
            string str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Towns;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var con = new SqlConnection(str);
            con.Open();
            var command = new SqlCommand("SELECT * FROM People",con);
            var er = command.ExecuteReader();
            er.Read();
            Console.WriteLine(er.GetDateTime(1).Year+" "+er.GetString(2));
            er.Close();
            con.Close();
            Console.ReadKey();
        }
    }
}
