using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure.Logging
{
    public class Logger
    {
        public DateTime date { get; set; }
        public string message { get; set; }
        public string StackTrace { get; set; }
    }
}
