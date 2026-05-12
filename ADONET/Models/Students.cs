using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string matricule { get; set; }

        public string? firstName { get; set; }

        public string lastName { get; set; }
    }
}
