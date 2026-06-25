using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;

namespace ShoesDb2026.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShoesDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ShoesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entidad)
        {
            _dbSet.Add(entidad);
        }
        public virtual void Delete(int id)
        {
            var entidad = _dbSet.Find(id);
            if (entidad is null)
            {
                throw new KeyNotFoundException($"No se pudo borrar la entidad ID: {id} de la tabla {typeof(T).Name}");
            }
            _dbSet.Remove(entidad);
        }

        public void Update(T entidad, int id)
        {
            var entidadEnDb = _dbSet.Find(id);
            if (entidadEnDb is null)
            {
                throw new KeyNotFoundException($"No se pudo actualizar la entidad ID: {id} de la tabla {typeof(T).Name}");
            }
            _dbSet.Entry(entidadEnDb).CurrentValues.SetValues(entidad);
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet
                .AsNoTracking()
                .ToList();
        }
        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
                
        }
    }
}
