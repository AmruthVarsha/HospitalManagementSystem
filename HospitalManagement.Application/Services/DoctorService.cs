using HospitalManagement.Infrastructure.Repositories;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Domain.Interfaces;

namespace HospitalManagement.Application.Services
{
    public class DoctorService : IDoctorService
    {
        //private DoctorRepositoryMemory DoctorRepository = new DoctorRepositoryMemory();
        private DoctorRepositoryADO DoctorRepository;

        public DoctorService(string connectionString)
        {
            DoctorRepository = new DoctorRepositoryADO(connectionString);
        }

        public bool AddDoctor(Doctor doctor)
        {
            if (string.IsNullOrEmpty(doctor.Name))
            {
                throw new InvalidDoctorException("Doctor name cannot be empty");
            }
            else if (doctor.ConsultationFee <= 0)
            {
                throw new InvalidDoctorException("Doctor fees must be a postive value greater than 0");
            }
            else if (string.IsNullOrEmpty(doctor.Specialization))
            {
                throw new InvalidDoctorException("Doctor specialization cannot be empty");
            }
            else
            {
                if (DoctorRepository.GetById(doctor.DoctorId) != null)
                {
                    throw new InvalidDoctorException("Doctor id is already in use");
                }
                else
                {
                    DoctorRepository.Add(doctor);
                }
            }
            return true;
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> list =  DoctorRepository.GetAll();

            if (list.Count == 0)
            {
                throw new DoctorNotFoundException("No doctors to display.");
            }
            return list;
        }
    }

}
