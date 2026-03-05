using System;
using System.Linq;
using System.Collections.Generic;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Application.Services;
using HospitalManagement.Infrastructure.Logging;
using Microsoft.Data.SqlClient;
using HospitalManagement.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Infrastructure.Repositories;


namespace HospitalManagement.ConsoleApp
{
    internal class Program
    {


        static void Main(string[] args)
        {
            bool endLoop = false;
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"appsettings.json", optional:false,reloadOnChange:true).Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
            }
            catch(DatabaseConnectionException e)
            {
                Console.WriteLine(e.Message);
                Logger.Log(e);
            }

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDoctorRepository, DoctorRepositoryEF>();
            services.AddScoped<IPatientRepository, PatientRepositoryEF>();

            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();

            var provider = services.BuildServiceProvider();

            var doctorService = provider.GetService<IDoctorService>();
            var patientService = provider.GetService<IPatientService>();

            IDoctorService doctorServiceMemory = new DoctorService(new DoctorRepositoryMemory());
            IPatientService patientServiceMemory = new PatientService(new PatientRepositoryMemory(),new DoctorRepositoryMemory());

            while (!endLoop)
            {
                
                try
                {
                    Console.WriteLine("\n========================================\n");
                    Console.WriteLine("1. Add Doctor");
                    Console.WriteLine("2. List Doctors");
                    Console.WriteLine("3. List Doctors by Fee");
                    Console.WriteLine("4. Add Patient");
                    Console.WriteLine("5. List Patients");
                    Console.WriteLine("6. Find Patient by name");
                    Console.WriteLine("7. List Patients by Doctor id");
                    Console.WriteLine("8. Edit Patient");
                    Console.WriteLine("9. Delete Patient");
                    Console.WriteLine("10. Add Doctor in DB");
                    Console.WriteLine("11. List Doctors in DB");
                    Console.WriteLine("12. List Doctors by fee in DB");
                    Console.WriteLine("13. Add Patient in DB");
                    Console.WriteLine("14. List Patients in DB");
                    Console.WriteLine("15. Find Patient by name in DB");
                    Console.WriteLine("16. Find Patient by Doctor Id in DB");
                    Console.WriteLine("17. Edit Patient in DB");
                    Console.WriteLine("18. Delete Patient in DB");
                    Console.WriteLine("19. Exit");
                    Console.Write("Choice: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddDoctor(doctorServiceMemory);
                            break;
                        case 2:
                            ListDoctors(doctorServiceMemory);
                            break;
                        case 3:
                            ListDoctorsByFee(doctorServiceMemory);
                            break;
                        case 4:
                            AddPatient(patientServiceMemory);
                            break;
                        case 5:
                            ListPatients(patientServiceMemory);
                            break;
                        case 6:
                            FindByName(patientServiceMemory);
                            break;
                        case 7:
                            GetPatientByDoctor(patientServiceMemory);
                            break;
                        case 8:
                            EditPatient(patientServiceMemory);
                            break;
                        case 9:
                            DeletePatient(patientServiceMemory);
                            break;
                        case 10:
                            AddDoctor(doctorService);
                            break;
                        case 11:
                            ListDoctors(doctorService);
                            break;
                        case 12:
                            ListDoctorsByFee(doctorService);
                            break;
                        case 13:
                            AddPatient(patientService);
                            break;
                        case 14:
                            ListPatients(patientService);
                            break;
                        case 15:
                            FindByName(patientService);
                            break;
                        case 16:
                            GetPatientByDoctor(patientService);
                            break;
                        case 17:
                            EditPatient(patientService);
                            break;
                        case 18:
                            DeletePatient(patientService);
                            break;
                        case 19:
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

        public static void AddDoctor(IDoctorService doctorService)
        {
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
        }

        public static void ListDoctors(IDoctorService doctorService)
        {
            Console.WriteLine("\n\n");
            List<Doctor> doctors = doctorService.GetDoctors();
            foreach (Doctor i in doctors)
            {
                Console.WriteLine($"Doctor Id: {i.DoctorId}\nDoctor Name: {i.Name}\nSpecialzation: {i.Specialization}\nConsultation Fee: {i.ConsultationFee:F2}");
                Console.WriteLine();
            }
        }

        public static void ListDoctorsByFee(IDoctorService doctorService)
        {
            Console.WriteLine("\n\n");
            List<Doctor> doctors = doctorService.SortDoctorsByFee();
            foreach (Doctor i in doctors)
            {
                Console.WriteLine($"Doctor Id: {i.DoctorId}\nDoctor Name: {i.Name}\nSpecialzation: {i.Specialization}\nConsultation Fee: {i.ConsultationFee:F2}");
                Console.WriteLine();
            }
        }

        public static void AddPatient(IPatientService patientService)
        {
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
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
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
        }

        public static void ListPatients(IPatientService patientService)
        {
            Console.WriteLine("\n\n");
            List<Patient> patients = patientService.GetAllPatients();
            foreach (Patient i in patients)
            {

                Console.WriteLine($"Patient Id: {i.PatientId}");
                Console.WriteLine($"Patient Name: {i.Name}");
                Console.WriteLine($"Patient Age: {i.Age}");
                Console.WriteLine($"Patient Condition: {i.Condition}");
                Console.WriteLine($"Appointment Date: {i.AppointmentDate}");
                Console.WriteLine($"Doctor ID: {i.DoctorId}");
                Console.WriteLine();
            }
        }

        public static void FindByName(IPatientService patientService)
        {
            Console.WriteLine("\n\n");
            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();
            List<Patient> foundPatients = patientService.FindPatientByName(name);
            foreach (Patient i in foundPatients)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine($"Patient Id: {i.PatientId}");
                Console.WriteLine($"Patient Name: {i.Name}");
                Console.WriteLine($"Patient Age: {i.Age}");
                Console.WriteLine($"Patient Condition: {i.Condition}");
                Console.WriteLine($"Appointment Date: {i.AppointmentDate}");
                Console.WriteLine($"Doctor ID: {i.DoctorId}");
            }
        }

        public static void GetPatientByDoctor(IPatientService patientService)
        {
            Console.WriteLine("\n\n");
            Console.Write("Enter Doctor Id: ");
            int doctorId = int.Parse(Console.ReadLine());
            List<Patient> foundPatients = patientService.GetPatientByDoctorId(doctorId);
            foreach (Patient i in foundPatients)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine($"Patient Id: {i.PatientId}");
                Console.WriteLine($"Patient Name: {i.Name}");
                Console.WriteLine($"Patient Age: {i.Age}");
                Console.WriteLine($"Patient Condition: {i.Condition}");
                Console.WriteLine($"Appointment Date: {i.AppointmentDate}");
                Console.WriteLine($"Doctor ID: {i.DoctorId}");
            }
        }
        public static void EditPatient(IPatientService patientService)
        {
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
        }

        public static void DeletePatient(IPatientService patientService)
        {
            Console.WriteLine("\n\n");
            Console.Write("Enter patient id to delete: ");
            int patientId = int.Parse(Console.ReadLine());
            bool deleted = patientService.DeletePatient(patientId);
            if (deleted)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("Successfully deleted");
            }
        }
    }
}

