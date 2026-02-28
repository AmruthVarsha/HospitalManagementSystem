using HospitalManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IPatientService
    {
        public bool AddPatient(Patient patient);
        public List<Patient> GetAllPatients();
        public Patient FindPatientByName(string name);
        public bool EditPatient(Patient patient);
        public bool DeletePatient(string patientId);
    }
}
