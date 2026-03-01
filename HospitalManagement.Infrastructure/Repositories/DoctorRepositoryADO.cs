using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class DoctorRepositoryADO : IRepository<Doctor>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public DoctorRepositoryADO(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Add(Doctor doctor)
        {
            connection.Open();
            command = new SqlCommand($"insert into Doctors(Name,Specialization,ConsultationFee) values (@Name,@Specialization,@ConsultationFee)", connection);
            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Doctor> GetAll()
        {
            connection.Open();
            command = new SqlCommand($"select * from Doctors",connection);
            reader = command.ExecuteReader();
            List<Doctor> result = new List<Doctor>();
            while (reader.Read())
            {
                result.Add(new Doctor() { DoctorId = Convert.ToInt32(reader[0]), Name = (string)reader[1], Specialization = (string)reader[2], ConsultationFee = Convert.ToDouble(reader[3]) });
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public Doctor GetById(int id)
        {
            connection.Open();
            command = new SqlCommand($"select * from Doctors where DoctorId=@DoctorId", connection);
            command.Parameters.AddWithValue("@DoctorId", id);
            reader = command.ExecuteReader();
            
            if (!reader.Read())
            {
                reader.Close();
                connection.Close();
                return null;
            }
            Doctor doctor = new Doctor() { DoctorId = Convert.ToInt32(reader[0]), Name = (string)reader[1], Specialization = (string)reader[2], ConsultationFee = Convert.ToDouble(reader[3]) };
            reader.Close();
            connection.Close();
            return doctor;
        }
        public void Update(Doctor doctor)
        {
            connection.Open();
            command = new SqlCommand($"update Doctors set Name=@Name,Specialization=@Specialization,ConsultationFee=@ConsultationFee where DoctorId=@DoctorId",connection);
            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
            command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(Doctor doctor)
        {
            connection.Open();
            command = new SqlCommand($"delete from Doctors where DoctorId=@DoctorId",connection);
            command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
