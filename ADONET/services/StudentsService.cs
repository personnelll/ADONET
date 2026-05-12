using ADONET.Interfaces;
using ADONET.Models;
using ADONET.repositories;
using Microsoft.Extensions.Logging;

namespace ADONET.services
{
    public class StudentsService : IStudentsService
    {
        private IStudentRepo _coursSGBDRepo;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger, IStudentRepo coursSGBDRepo)
        {
            _logger = logger;

            _coursSGBDRepo = coursSGBDRepo;

        }

        public List<Students> GetAll()
        {
            _logger.LogInformation("Entering GetAll method in StudentsService");
            List<Students> students = _coursSGBDRepo.GetAll();
            _logger.LogInformation($"Exiting GetAll method in StudentsService");
            return students;
        }

        public void Add(Students student)
        {
            checkMatricule(student.matricule);

            checkLastName(student.firstName);

            _coursSGBDRepo.Add(student);
        }

        public void Delete(int id)
        {
            _coursSGBDRepo.Delete(id);
        }

        public void Update(Students student)
        {
            checkLastName(student.lastName);

            checkMatricule(student.matricule);

            _coursSGBDRepo.Update(student);
        }

        public List<Students> GetByLastName(string lastName)
        {
            checkLastName(lastName);
            List<Students> students = _coursSGBDRepo.GetByLastName(lastName);
            return students;
        }

        private void checkMatricule(string matricule)
        {
            if (string.IsNullOrEmpty(matricule))
            {
                throw new ArgumentException("Matricule cannot be null or empty.");
            }
            string prefixe = matricule.Substring(0, 2);

            if (prefixe != "HE" && prefixe != "PS")
            {
                throw new ArgumentException("Matricule must start with HE or PS");
            }
        }

        private void checkLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("checkLastName cannot be null or empty.");
            }
        }


    }
}
