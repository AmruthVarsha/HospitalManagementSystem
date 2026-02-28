using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Exceptions
{
    public class InvalidPatientException : Exception
    {
        public InvalidPatientException() : base() { }
        public InvalidPatientException(string message) : base(message) { }
    }
}
