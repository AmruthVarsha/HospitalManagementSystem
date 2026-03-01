using System;
using System.Linq;
using System.Collections.Generic;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Application.Services;
using HospitalManagement.Infrastructure.Logging;
using Microsoft.Data.SqlClient;
using HospitalManagement.Domain.Exceptions;


namespace HospitalManagement.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endLoop = false;
            
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=AppDB;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
            }
            catch(DatabaseConnectionException e)
            {
                Console.WriteLine(e.Message);
                Logger.Log(e);
            }

            DoctorService doctorService = new DoctorService(connectionString);
            PatientService patientService = new PatientService(connectionString);

            while (!endLoop)
            {
                
                try
                {
                    Console.WriteLine("\n========================================\n");
                    Console.WriteLine("1. Add Doctor");
                    Console.WriteLine("2. List Doctors");
                    Console.WriteLine("3. Add Patient");
                    Console.WriteLine("4. List Patients");
                    Console.WriteLine("5. Find Patient");
                    Console.WriteLine("6. Edit Patient");
                    Console.WriteLine("7. Delete Patient");
                    Console.WriteLine("8. Exit");
                    Console.Write("Choice: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\n\n");
                            Doctor doctor = new Doctor();
                            Console.Write("Enter doctor Name: ");
                            doctor.Name = Console.ReadLine();
                            Console.Write("Enter doctor Specialization: ");
                            doctor.Specialization = Console.ReadLine();
                            Console.Write("Enter Consultation fee: ");
                            doctor.ConsultationFee = decimal.Parse(Console.ReadLine());
                            bool addedDoctor = doctorService.AddDoctor(doctor);
                            if (addedDoctor)
                            {
                                Console.WriteLine("\n\n");
                                Console.WriteLine("Succesfully added");
                            }
                            break;
                        case 2:
                            Console.WriteLine("\n\n");
                            List<Doctor> doctors = doctorService.GetDoctors();
                            foreach(Doctor i in doctors)
                            {
                                Console.WriteLine($"Doctor Id: {i.DoctorId}\nDoctor Name: {i.Name}\nSpecialzation: {i.Specialization}\nConsultation Fee: {i.ConsultationFee:F2}");
                                Console.WriteLine();
                            }
                            break;
                        case 3:
                            Console.WriteLine("\n\n");
                            Patient patient = new Patient();
                            Console.Write("Enter patient Name: ");
                            patient.Name = Console.ReadLine();
                            Console.Write("Enter patient Age: ");
                            patient.Age = int.Parse(Console.ReadLine());
                            Console.Write("Enter patient condition: ");
                            patient.Condition = Console.ReadLine();
                            Console.Write("Enter Appointment Date (dd-mm-yyyy HH:mm:ss): ");
                            DateTime date;
                            if(DateTime.TryParse(Console.ReadLine(),out date)){
                                patient.AppointmentDate = date;
                            }
                            else
                            {
                                Console.WriteLine("\n\n");
                                throw new FormatException("Wrong date format.");
                            }
                            Console.Write("Enter Doctor Id: ");
                            patient.DoctorId = int.Parse(Console.ReadLine());
                            bool addedPatient = patientService.AddPatient(patient);
                            if (addedPatient)
                            {
                                Console.WriteLine("\n\n");
                                Console.WriteLine("Succesfully added");
                            }
                            break;
                        case 4:
                            Console.WriteLine("\n\n");
                            List<Patient> patients = patientService.GetAllPatients();
                            foreach(Patient i in patients)
                            {
                        
                                Console.WriteLine($"Patient Id: {i.PatientId}");
                                Console.WriteLine($"Patient Name: {i.Name}");
                                Console.WriteLine($"Patient Age: {i.Age}");
                                Console.WriteLine($"Patient Condition: {i.Condition}");
                                Console.WriteLine($"Appointment Date: {i.AppointmentDate}");
                                Console.WriteLine($"Doctor ID: {i.DoctorId}");
                                Console.WriteLine();
                            }
                            break;
                        case 5:
                            Console.WriteLine("\n\n");
                            Console.Write("Enter Patient Name: ");
                            string name = Console.ReadLine();
                            Patient foundPatient = patientService.FindPatientByName(name);
                            Console.WriteLine("\n\n");
                            Console.WriteLine($"Patient Id: {foundPatient.PatientId}");
                            Console.WriteLine($"Patient Name: {foundPatient.Name}");
                            Console.WriteLine($"Patient Age: {foundPatient.Age}");
                            Console.WriteLine($"Patient Condition: {foundPatient.Condition}");
                            Console.WriteLine($"Appointment Date: {foundPatient.AppointmentDate}");
                            Console.WriteLine($"Doctor ID: {foundPatient.DoctorId}");
                            break;
                        case 6:
                            Console.WriteLine("\n\n");
                            Patient newPatient = new Patient();
                            Console.Write("Enter patient Id: ");
                            newPatient.PatientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter patient Name: ");
                            newPatient.Name = Console.ReadLine();
                            Console.Write("Enter patient Age: ");
                            newPatient.Age = int.Parse(Console.ReadLine());
                            Console.Write("Enter patient condition: ");
                            newPatient.Condition = Console.ReadLine();
                            Console.Write("Enter Appointment Date (dd-mm-yyyy HH:mm:ss): ");
                            DateTime newDate;
                            if (DateTime.TryParse(Console.ReadLine(), out newDate))
                            {
                                newPatient.AppointmentDate = newDate;
                            }
                            else
                            {
                                Console.WriteLine("\n\n");
                                throw new FormatException("Wrong date format.");
                            }
                            Console.Write("Enter Doctor Id: ");
                            newPatient.DoctorId = int.Parse(Console.ReadLine());
                            bool edited = patientService.EditPatient(newPatient);
                            if (edited)
                            {
                                Console.WriteLine("\n\n");
                                Console.WriteLine("Succesfully edited");
                            }
                            break;
                        case 7:
                            Console.WriteLine("\n\n");
                            Console.Write("Enter patient id to delete: ");
                            int patientId = int.Parse(Console.ReadLine());
                            bool deleted = patientService.DeletePatient(patientId);
                            if (deleted)
                            {
                                Console.WriteLine("\n\n");
                                Console.WriteLine("Successfully deleted");
                            }
                            break;
                        case 8:
                            endLoop = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Logger.Log(e);
                }
            }
        }
    }
}

