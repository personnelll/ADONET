using Interfaces;
using Models;
using Microsoft.Extensions.Logging;
using DTO;
using AutoMapper;

namespace ServicesDLL
{
    public class KotService : IKotService
    {
        private IKotRepo _kotRepo;
        private readonly ILogger<KotService> _logger;
        private readonly IMapper _mapper;

        public KotService(ILogger<KotService> logger, IKotRepo coursSGBDRepo, IMapper mapper)
        {
            _logger = logger;

            _kotRepo = coursSGBDRepo;

            _mapper = mapper;
        }

        public List<Kots> GetAll()
        {
            _logger.LogInformation("Entering GetAll method in KotsService");
            List<KotStudentDTO> kotsDTO = _kotRepo.GetAll();
            List<Kots> kots = _mapper.Map<List<Kots>>(kotsDTO);

            _logger.LogInformation($"Exiting GetAll method in KotsService");
            return kots;
        }

        public void Add(Kots kot)
        {
            _kotRepo.Add(kot);
        }

        public void Delete(int id)
        {
            _kotRepo.Delete(id);
        }

        public void Update(Kots kot)
        {

            _kotRepo.Update(kot);
        }

        public List<Kots> GetByName(string name)
        {
            List<Kots> kots = _kotRepo.GetByName(name);
            return kots;
        }
    }
}
