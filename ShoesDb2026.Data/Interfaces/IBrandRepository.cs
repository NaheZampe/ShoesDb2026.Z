using ShoesDb2026.Entities;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IBrandRepository:IConcurrentRepository<Brand>
    {
        bool Exist (Brand brand);
        bool IsRelated (Brand brand);
    }
}
