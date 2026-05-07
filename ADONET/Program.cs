// See https://aka.ms/new-console-template for more information

using ADONET.services;
using ADONET.Models;
using Microsoft.Extensions.Logging;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});
ILogger logger = loggerFactory.CreateLogger<Program>();

try {

    ILogger<StudentsService> studentsLogger = loggerFactory.CreateLogger<StudentsService>();

    StudentsService studentsService = new StudentsService(studentsLogger);

    logger.LogInformation("Fetching all students...");
    List<Students> students = studentsService.GetAll();

    foreach (var student in students)
    {
       logger.LogInformation($"{student.matricule}, {student.firstName}, {student.lastName}, {student.email}");
    }
    Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred");
}


