using ADONET.Models;

namespace ADONET.Interfaces
{
    public interface IStudentsService
    {
        public List<Students> GetAll();
        public void Add(Students student);
    }
}
