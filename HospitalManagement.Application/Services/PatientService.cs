using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Infrastructure.Repositories;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Exceptions;

namespace HospitalManagement.Application.Services
{
    public class PatientService : IPatientService
    {
        private PatientRepositoryMemory PatientRepository = new PatientRepositoryMemory();
        private DoctorRepositoryMemory DoctorRepository = new DoctorRepositoryMemory();
        public bool AddPatient(Patient patient)
        {
            if(string.IsNullOrEmpty(patient.PatientId ))
            {
                throw new InvalidPatientException("Patient id cannot be empty");
            }
            else if (patient.Age <= 0)
            {
                throw new InvalidPatientException("Patient age must be a positive integer.");
            }
            else if (DoctorRepository.GetById(patient.DoctorId) == null)
            {
                throw new DoctorNotFoundException("invalid doctor id, doctor not found");
            }
            else if (patient.AppointmentDate < DateTime.Now)
            {
                throw new InvalidPatientException("date cannot be past date");
            }
            else
            {
                if (PatientRepository.GetById(patient.PatientId) != null)
                {
                    throw new InvalidPatientException("Patient id already in use");
                }
                else
                {
                    PatientRepository.Add(patient);
                }
            }
            return true;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> list =  PatientRepository.GetAll();
            if (list.Count == 0)
            {
                throw new PatientNotFoundException("No patients to display.");
            }
            return list;
        }

        public Patient FindPatientByName(string name)
        {
            List<Patient> patients = PatientRepository.GetAll();
            Patient patient = patients.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            if (patient == null)
            {
                throw new PatientNotFoundException($"Patient with name: {name} not found.");
            }
            return patient;
        }

        public bool EditPatient(Patient patient)
        {
            if (string.IsNullOrEmpty(patient.PatientId))
            {
                throw new InvalidPatientException("Patient id cannot be empty");
            }

            Patient hasPatient = PatientRepository.GetById(patient.PatientId);

            if (hasPatient == null)
            {
                throw new PatientNotFoundException($"Patient with id: {patient.PatientId} not found.");
            }

            if (patient.Age <= 0)
            {
                throw new InvalidPatientException("Patient age must be a positive integer.");
            }
            else if (DoctorRepository.GetById(patient.DoctorId) == null)
            {
                throw new DoctorNotFoundException("invalid doctor id, doctor not found");
            }
            else if (patient.AppointmentDate < DateTime.Now)
            {
                throw new InvalidPatientException("date cannot be past date");
            }
            
            PatientRepository.Update(patient);
            return true;
        }

        public bool DeletePatient(string patientId)
        {
            Patient patient = PatientRepository.GetById(patientId);
            if (patient == null)
            {
                throw new PatientNotFoundException($"Patient with id: {patient.PatientId} not found.");
            }
            PatientRepository.Delete(patient);
            return true;
        }
    }
}
