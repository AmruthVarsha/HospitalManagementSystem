using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Domain.Entities;

namespace HospitalManagement.Infrastructure.Data
{
    internal class AppDbContext : DbContext
    {
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Patient> Patients { get; set; }
    }
}
