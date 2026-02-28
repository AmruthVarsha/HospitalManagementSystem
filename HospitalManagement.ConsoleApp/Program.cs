using System;
using System.Linq;
using System.Collections.Generic;
using HospitalManagement.Domain.Entities;


namespace HospitalManagement.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endLoop = false;

            while (!endLoop)
            {
                try
                {
                    Console.WriteLine("1. Add Doctor\n2. Exit");
                    Console.Write("Choice: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Doctor doctor = new Doctor();
                            Console.Write("Enter doctor Id: ");
                            doctor.DoctorId = Console.ReadLine();
                            Console.Write("Enter doctor Name: ");
                            doctor.Name = Console.ReadLine();
                            Console.Write("Enter doctor Specialization: ");
                            doctor.Specialization = Console.ReadLine();
                            Console.Write("Enter Consultation fee: ");
                            doctor.ConsultationFee = double.Parse(Console.ReadLine());


                            break;
                        case 2:
                            endLoop = true;
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

