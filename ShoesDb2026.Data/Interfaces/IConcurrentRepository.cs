using ShoesDb2026.Entities.Interfaces;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IConcurrentRepository<T>:IGenericRepository<T> where T : class,IConcurrencyEntity 
    {
        void Delete(int id, byte[] rowVersion);
        void Update(T entidad, int id, byte[] rowVersion);
    }
}
