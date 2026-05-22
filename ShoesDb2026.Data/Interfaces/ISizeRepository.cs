using ShoesDb2026.Entities;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISizeRepository
    {
        List<SiZe> GetAll();
        void Update(SiZe size);
        SiZe? GetById(int id);
        bool Exist(decimal number, int? sizeId=null);
    }
}
