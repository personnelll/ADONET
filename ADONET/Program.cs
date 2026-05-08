// See https://aka.ms/new-console-template for more information

using ADONET.services;
using ADONET.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ADONET.Interfaces;
using ADONET.repositories;

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
            logger.LogInformation("Fetching all studentd...");
            studentsService.GetAll();
            logger.LogInformation("students fetched successfuly.");
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

