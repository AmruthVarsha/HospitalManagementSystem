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
    public class DoctorRepositoryEF : IRepository<Doctor>
    {
        private AppDbContext _context = new AppDbContext();
        public void Add(Doctor doctor) {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
        public List<Doctor> GetAll() {
            List<Doctor> result = _context.Doctors.ToList();
            if (result.Count == 0) {
                return null;
            }
            return result;
        }
        public Doctor GetById(int id) {
            Doctor doctor = _context.Doctors.Find(id);
            return doctor;
        }
        public void Update(Doctor doctor){
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }
        public void Delete(Doctor doctor) {
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }
    }
}
