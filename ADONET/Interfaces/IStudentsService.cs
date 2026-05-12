using ADONET.Models;

namespace ADONET.Interfaces
{
    public interface IStudentsService
    {
        List<Students> GetAll();
        void Add(Students student);

        void Delete(int id);

        void Update(Students student);

        List<Students> GetByLastName(string lastName);
    }
}
