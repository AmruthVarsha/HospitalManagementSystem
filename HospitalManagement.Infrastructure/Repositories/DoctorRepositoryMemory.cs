using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class DoctorRepositoryMemory : IRepository<Doctor>
    {
        private static List<Doctor> _doctors = new List<Doctor>();

        public void Add(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetById(int id)
        {
            Doctor doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            return doctor;
        }

        public void Update(Doctor doctor)
        {
            for(int i = 0; i < _doctors.Count; i++)
            {
                if (doctor.DoctorId == _doctors[i].DoctorId)
                {
                    _doctors[i] = doctor;
                    break;
                }
            }
        }

        public void Delete(Doctor doctor)
        {
            _doctors.Remove(doctor);
        }
    }
}
