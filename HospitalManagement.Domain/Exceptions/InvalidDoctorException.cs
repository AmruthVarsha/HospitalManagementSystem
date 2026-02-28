using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Exceptions
{
    public class InvalidDoctorException : Exception
    {
        public InvalidDoctorException() : base(){ }
        public InvalidDoctorException(string message) : base(message){ }
    }
}
