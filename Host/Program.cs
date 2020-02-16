using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(TestTaskKost_WCF.DataBase_Kosta)))
            {
                host.Open();
                Console.WriteLine("Start");
                Console.ReadLine();
            }
        }
    }
}
