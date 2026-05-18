using AutoMapper;
using DTO;
using Models;

namespace Profiles
{
    public class KotProfile : Profile
    {
        public KotProfile()
        {
            CreateMap<KotStudentDTO, Students>()
                .ForMember(d => d.matricule, opt => opt.MapFrom(s => s.ETU_MATRICULE))
                .ForMember(d => d.lastName, opt => opt.MapFrom(s => s.ETU_NOM))
                .ForMember(d => d.firstName, opt => opt.MapFrom(s => s.ETU_PRENOM));

            CreateMap<KotStudentDTO, Kots>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.KOT_id))
                .ForMember(d => d.nom, opt => opt.MapFrom(s => s.KOT_name))
                .ForMember(d => d.Resident, opt => opt.MapFrom(s => s));
        }
    }
}