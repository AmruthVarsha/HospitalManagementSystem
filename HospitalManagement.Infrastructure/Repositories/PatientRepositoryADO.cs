using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace HospitalManagement.Infrastructure.Repositories
{
    public class PatientRepositoryADO : IRepository<Patient>
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public PatientRepositoryADO(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Add(Patient patient)
        {
            connection.Open();
            command = new SqlCommand($"insert into Patients(Name,Age,Condition,AppointmentDate,DoctorId) values (@Name,@Age,@Condition,@AppointmentDate,@DoctorId)", connection);
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Condition", patient.Condition);
            command.Parameters.AddWithValue("@AppointmentDate", patient.AppointmentDate);
            command.Parameters.AddWithValue("@DoctorId", patient.DoctorId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Patient> GetAll()
        {
            connection.Open();
            command = new SqlCommand($"select * from Patients",connection);
            reader = command.ExecuteReader();
            List<Patient> result = new List<Patient>();
            while (reader.Read())
            {
                result.Add(new Patient() { PatientId = Convert.ToInt32(reader[0]), Name = (string)reader[1],Age=Convert.ToInt32(reader[2]), Condition = (string)reader[3], AppointmentDate = Convert.ToDateTime(reader[4]), DoctorId = Convert.ToInt32(reader[5]) });
            }
            reader.Close();
            connection.Close();
            return result;
        }

        public Patient GetById(int id)
        {
            connection.Open();
            command = new SqlCommand($"select * from Patients where PatientId=@PatientId", connection);
            command.Parameters.AddWithValue("@PatientId", id);
            reader = command.ExecuteReader();
            
            if (!reader.Read())
            {
                reader.Close();
                connection.Close();
                return null;
            }
            Patient patient = new Patient() { PatientId = Convert.ToInt32(reader[0]), Name = (string)reader[1], Age = Convert.ToInt32(reader[2]), Condition = (string)reader[3], AppointmentDate= Convert.ToDateTime(reader[4]), DoctorId = Convert.ToInt32(reader[5]) };
            reader.Close();
            connection.Close();
            return patient;
        }

        public void Update(Patient patient) {
            connection.Open();
            command = new SqlCommand($"update Patients set Name=@Name,Age=@Age,Condition=@Condition,AppointmentDate=@AppointmentDate,DoctorId=@DoctorId where PatientId=@PatientId",connection);
            command.Parameters.AddWithValue("@PatientId", patient.PatientId);
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Condition", patient.Condition);
            command.Parameters.AddWithValue("@AppointmentDate", patient.AppointmentDate);
            command.Parameters.AddWithValue("@DoctorId", patient.DoctorId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Patient patient)
        {
            connection.Open();
            command = new SqlCommand("delete from Patients where PatientId=@PatientId",connection);
            command.Parameters.AddWithValue("@PatientId", patient.PatientId);
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
