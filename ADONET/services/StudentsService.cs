using ADONET.Models;
using ADONET.repositories;
using Microsoft.Extensions.Logging;

namespace ADONET.services
{
    public class StudentsService
    {
        private CoursSGBDRepo _coursSGBDRepo;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger) 
        {
            _logger = logger;

            _coursSGBDRepo = new CoursSGBDRepo();

        }

        public List<Students> GetAll()
        {
            //_logger.LogDebug("Entering GetAll method in StudentsService");
            List<Students> students = _coursSGBDRepo.GetAll();            
            return students;
        }
    
            //public void GetStudentById(int id)
            //{
            //    Console.WriteLine($"Getting student with ID: {id}...");
            //    // Code to retrieve and display a student by their ID from the database
            //}
    
            //public void AddStudent(string name, int age)
            //{
            //    Console.WriteLine($"Adding student: Name={name}, Age={age}...");
            //    // Code to add a new student to the database
            //}
    
            //public void UpdateStudent(int id, string name, int age)
            //{
            //    Console.WriteLine($"Updating student with ID: {id} to Name={name}, Age={age}...");
            //    // Code to update an existing student's information in the database
            //}
    
            //public void DeleteStudent(int id)
            //{
            //    Console.WriteLine($"Deleting student with ID: {id}...");
            //// Code to delete a student from the database by their ID
        //}
    }
}
