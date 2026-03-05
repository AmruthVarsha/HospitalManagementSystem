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
        public List<Patient> FindPatientByName(string name);
        public List<Patient> GetPatientByDoctorId(int doctorId);
        public bool EditPatient(Patient patient);
        public bool DeletePatient(int patientId);
    }
}
