using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities.Interfaces;

namespace ShoesDb2026.Data.Repositories
{
    public class ConcurrentRepository<T> : GenericRepository<T>,
        IConcurrentRepository<T> where T : class, IConcurrencyEntity
    {
        public ConcurrentRepository(ShoesDbContext context) : base(context)
        {
        }
        public override void Delete(int id)
        {
            throw new NotImplementedException("Debe utilizar la versión de concurrencia");
        }
        public void Delete(int id, byte[] rowVersion)
        {
            var entidadEnDb = _dbSet.Find(id);
            if (entidadEnDb is null)
            {
                throw new KeyNotFoundException($"No se pudo borrar la entidad ID: {id} de la tabla {typeof(T).Name}");
            }

            var entidad = _context.Entry(entidadEnDb);
            entidad.OriginalValues["RowVersion"] = rowVersion;
            _dbSet.Remove(entidadEnDb);
        }

        public void Update(T entidad, int id, byte[] rowVersion)
        {
            var entidadEnDb = _dbSet.Find(id);

            if (entidadEnDb is null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró la entidad ID:{id}");
            }

            var entry = _context.Entry(entidadEnDb);

            entry.OriginalValues["RowVersion"] = rowVersion;

            entry.CurrentValues.SetValues(entidad);
        }
    }
}
