// See https://aka.ms/new-console-template for more information

using ADONET.services;
using ADONET.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ADONET.Interfaces;
using ADONET.repositories;
using Microsoft.VisualBasic.FileIO;

//var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder.AddConsole();
//    builder.SetMinimumLevel(LogLevel.Debug);
//});
//ILogger logger = loggerFactory.CreateLogger<Program>();

//try {

//    ILogger<StudentsService> studentsLogger = loggerFactory.CreateLogger<StudentsService>();

//    StudentsService studentsService = new StudentsService(studentsLogger);

//    logger.LogInformation("Fetching all students...");
//    List<Students> students = studentsService.GetAll();

//    foreach (var student in students)
//    {
//       logger.LogInformation($"{student.matricule}, {student.firstName}, {student.lastName}, {student.email}");
//    }
//    Console.WriteLine("Hello, World!");
//}
//catch (Exception ex)
//{
//    logger.LogError(ex, "An error occurred");
//}

namespace ADONET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var serviceProvider = ConfigureService();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var studentsService = serviceProvider.GetRequiredService<IStudentsService>();

            int choice = Convert.ToInt32(args[0]);

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
            studentsService.Remove(10);
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

        private static ServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<ICoursSGBDRepo, CoursSGBDRepo>()
                    .AddSingleton<IStudentsService, StudentsService>();
            return services.BuildServiceProvider();
        }
    }
}

