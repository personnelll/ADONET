using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record KotStudentDTO(int KOT_id, string KOT_name, string ETU_MATRICULE, string ETU_NOM, string ETU_PRENOM);

}