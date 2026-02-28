using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() : base(){ }
        public PatientNotFoundException(string message) : base(message){ }
    }
}
