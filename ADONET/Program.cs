// See https://aka.ms/new-console-template for more information

using ADONET.services;
using ADONET.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ADONET.Interfaces;
using ADONET.repositories;
using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace ADONET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var serviceProvider = ConfigureService();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();

            Console.WriteLine("Select an action : ");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Delete");
            Console.WriteLine("3 Add and check Matricule");
            Console.WriteLine("4 - Update Student");
            Console.WriteLine("Your choice");

            var input = Console.ReadLine();

            int choice;

            if (!int.TryParse(input, out choice))
            {
                logger.LogWarning("Invalid input. Please enter a number corresponding to the action.");
                return;
            }



            try
            {
                switch (choice)
                {
                    case 1: Add(studentsService);
                        break;
                    case 2: Delete(studentsService); 
                        break;
                    default:
                        logger.LogWarning("Invalid choice. Please select 1 to add or 2 to delete.");
                        break;
                    case 3:AddCheckMatricule(studentsService);
                        break;
                    case 4: Update(studentsService);
                        break;
                }


                logger.LogInformation("Fetching all studentd...");
                studentsService.GetAll();
                logger.LogInformation("students fetched successfuly.");

                
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occured while processing students.");
            }
            
        }

        private static void AddCheckMatricule(IStudentsService studentsService)
        {
            Students newStudent = new Students();
            newStudent.matricule = "PS04";
            newStudent.firstName = "Quentin";
            newStudent.lastName = "Platiau";
            studentsService.Add(newStudent);
        }
        private static void Delete(IStudentsService studentsService)
        {
            studentsService.Delete(10);
        }

        private static void Add(IStudentsService studentsService)
        {
            Students newStudent = new Students();
            newStudent.matricule = "04";
            newStudent.firstName = "Denis";
            newStudent.lastName = "Platiau";
            studentsService.Add(newStudent);
            
            newStudent.matricule = "05";
            newStudent.firstName = "Arlette";
            newStudent.lastName = "Pironet";
            studentsService.Add(newStudent);
        }

        private static void Update(IStudentsService studentsService)
        {
            Console.WriteLine("Enter the ID of the student to update : ");
            var input = Console.ReadLine();
            if(int.TryParse(input, out int id))
            {
                Students updateStudent = new Students();
                updateStudent.Id = id;
                updateStudent.firstName = "Arlette";
                updateStudent.lastName = "Pironet";
                updateStudent.matricule = "PS05";
                studentsService.Update(updateStudent);
            }
            else
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
            }
            
        }

        private static ServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<IStudentRepo, StudentRepo>()
                    .AddSingleton<IStudentsService, StudentsService>();
            return services.BuildServiceProvider();
        }
    }
}

