using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() : base(){ }
        public DatabaseConnectionException(string message) : base(message){ }
    }
}
