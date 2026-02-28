using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HospitalManagement.Infrastructure.Logging
{
    public class Logger
    {
        public DateTime date { get; set; }
        public string message { get; set; }
        public string StackTrace { get; set; }

        public static void Log(Exception e)
        {
            using (StreamWriter sw = new StreamWriter("errorlog.txt", true))
            {
               
                sw.WriteLine($"{DateTime.Now} {e.Message} {e.StackTrace}");
                sw.WriteLine("\n=========================================\n");
                
            }
        }
    }
}
