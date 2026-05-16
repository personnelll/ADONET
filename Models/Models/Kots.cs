using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Kots
    {
        public int Id { get; set; }
        public string nom { get; set; }

        public Students? Resident { get; set; }

    }
}
