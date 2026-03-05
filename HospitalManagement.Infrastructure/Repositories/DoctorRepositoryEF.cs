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
    public class DoctorRepositoryEF : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepositoryEF(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor doctor) {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
        public List<Doctor> GetAll() {
            List<Doctor> result = _context.Doctors.Include(d => d.Patients).ToList();
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

        public List<Doctor> SortByFee()
        {
            return _context.Doctors.OrderBy(d => d.ConsultationFee).ToList();
        }

       
    }
}
