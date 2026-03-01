using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class PatientRepositoryEF : IRepository<Patient>
    {
        private AppDbContext _context = new AppDbContext();

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public List<Patient> GetAll()
        {
            List<Patient> result = _context.Patients.ToList();
            if (result.Count == 0)
            {
                return null;
            }
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
    }
}
