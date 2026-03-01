using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Interfaces;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class PatientRepositoryMemory : IRepository<Patient>
    {
        private static List<Patient> _patients = new List<Patient>();

        public void Add(Patient patient)
        {
            _patients.Add(patient);
        }

        public List<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetById(int id)
        {
            Patient patient = _patients.FirstOrDefault(p => p.PatientId== id);
            return patient;
        }

        public void Update(Patient patient)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (patient.PatientId == _patients[i].PatientId)
                {
                    _patients[i] = patient;
                }
            }
        }

        public void Delete(Patient patient)
        {
            _patients.Remove(patient);
        }
    }
}
