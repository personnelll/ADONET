using Models;
using DTO;

namespace Interfaces
{
    public interface IKotRepo
    {
        public List<KotStudentDTO> GetAll();
        public void Add(Kots kot);

        public void Delete(int id);

        public void Update(Kots kot);

        List<Kots> GetByName(string name);
    }
}
