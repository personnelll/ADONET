using ServicesDLL;
using Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Interfaces;
using Repositories;
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
            Console.WriteLine("5 - Get all students");
            Console.WriteLine("6 - Get students per lastName");
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
                    case 5: GetAll(studentsService);
                        break;
                    case 6: GetByLastName(studentsService);
                        break;
                }                
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
            newStudent.matricule = "HE04";
            newStudent.firstName = "Denis";
            newStudent.lastName = "Platiau";
            studentsService.Add(newStudent);
            
            newStudent.matricule = "PS05";
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

        private static void GetAll(IStudentsService studentsService)
        {
            var students = studentsService.GetAll();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Matricule: {student.matricule}, Name: {student.firstName} {student.lastName}");
            }
        }

        private static void GetByLastName(IStudentsService studentsService)
        {
            Console.WriteLine("Enter the last name to search for : ");
            var lastName = Console.ReadLine();
            if (lastName == null)
                {
                Console.WriteLine("Invalid last name. Please enter a valid string.");
                return;
            }

            var students = studentsService.GetByLastName(lastName);

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Matricule: {student.matricule}, Name: {student.firstName} {student.lastName}");
            }
        }

        private static ServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<IStudentRepo, StudentDapperRepo>()
                    .AddSingleton<IStudentsService, StudentsService>();
            return services.BuildServiceProvider();
        }
    }
}

