using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class PatientRepositoryEF : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepositoryEF(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public List<Patient> GetAll()
        {
            List<Patient> result = _context.Patients.Include(p => p.Doctor).ToList();
            return result;
        }

        public Patient GetById(int id)
        {
            Patient patient = _context.Patients.Find(id);
            return patient;
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public List<Patient> GetByName(string name)
        {
            return _context.Patients.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
        }

        public List<Patient> GetPatientByDoctor(int doctorId)
        {
            return _context.Patients.Where(p => p.DoctorId == doctorId).ToList();
        }

    }
}
