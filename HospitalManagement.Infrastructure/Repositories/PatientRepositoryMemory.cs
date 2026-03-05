using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class PatientRepositoryMemory : IPatientRepository
    {
        private static int Id = 1;
        private static List<Patient> _patients = new List<Patient>();

        public void Add(Patient patient)
        {
            patient.PatientId = Id++;
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

        public List<Patient> GetByName(string name)
        {
            return _patients.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
        }

        public List<Patient> GetPatientByDoctor(int doctorId)
        {
            return _patients.Where(p => p.DoctorId== doctorId).ToList();
        }


    }
}
