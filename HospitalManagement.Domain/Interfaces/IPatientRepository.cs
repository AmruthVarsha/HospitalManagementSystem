using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Domain.Entities;
namespace HospitalManagement.Domain.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public List<Patient> GetByName(string name);
        public List<Patient> GetPatientByDoctor(int doctorId);

    }
}
