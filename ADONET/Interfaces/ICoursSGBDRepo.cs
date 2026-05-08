using ADONET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Interfaces
{
    public interface ICoursSGBDRepo
    {
        public List<Students> GetAll();
        public void Add(Students student);

        public void Remove(int id);
    }
}
