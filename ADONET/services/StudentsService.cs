using ADONET.Interfaces;
using ADONET.Models;
using ADONET.repositories;
using Microsoft.Extensions.Logging;

namespace ADONET.services
{
    public class StudentsService : IStudentsService
    {
        private ICoursSGBDRepo _coursSGBDRepo;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger, ICoursSGBDRepo coursSGBDRepo)
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
            _coursSGBDRepo.Add(student);
        }
    }
}
