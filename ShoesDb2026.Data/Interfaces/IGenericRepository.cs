namespace ShoesDb2026.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        IQueryable<T> Query();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity, int id);
        void Delete(int id);

    }
}
