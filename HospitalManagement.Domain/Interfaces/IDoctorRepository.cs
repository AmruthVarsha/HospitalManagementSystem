using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Domain.Entities;
namespace HospitalManagement.Domain.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        public List<Doctor> SortByFee();
    }
}
