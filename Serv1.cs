using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceLibrary1.ServiceReference1;

namespace WcfServiceLibrary1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Serv1 : IServ1
    {
        public static Service1Client client = new Service1Client();
        public static List<Row> rows = new List<Row>();
        public static int piece = 5;

        public string GetQuestion(int index)
        {
            if(index > rows.Count-1)
            {
                var newrows = client.GetRows(index/piece);
                int len = newrows.Length;
                for(int i=0;i<len;i++)
                {
                    rows.Add(newrows[i]);
                }
            }
            string question = rows[index].ToString();
            return question;
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
