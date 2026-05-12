using ADONET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET.Interfaces
{
    public interface IStudentRepo
    {
        public List<Students> GetAll();
        public void Add(Students student);

        public void Delete(int id);

        public void Update(Students student);

        List<Students> GetByLastName(string lastName);
    }
}
