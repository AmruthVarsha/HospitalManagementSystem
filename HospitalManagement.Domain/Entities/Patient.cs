using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Entities
{
    public class Patient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Condition { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DoctorId { get; set; }
    }
}
