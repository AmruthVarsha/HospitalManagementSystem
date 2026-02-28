using HospitalManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IDoctorService
    {
        public bool AddDoctor(Doctor doctor);
        public List<Doctor> GetDoctors();
    }
}
