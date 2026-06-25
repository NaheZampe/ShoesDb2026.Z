using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;

namespace ShoesDb2026.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoesDbContext _context;
        public IBrandRepository Brands { get; }
        public IGenreRepository Genres { get; }
        public ISportRepository Sports { get; }
        public IShoeRepository Shoes { get; }
        public ISizeRepository Sizes { get; }

        public UnitOfWork(IBrandRepository brand, IGenreRepository genre, ISportRepository sport, IShoeRepository shoe, ISizeRepository size, ShoesDbContext context)
        {
            Brands = brand;
            Genres = genre;
            Sports = sport;
            Shoes = shoe;
            Sizes = size;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {
            foreach (var item in _context.ChangeTracker.Entries())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.State = EntityState.Unchanged;
                        item.CurrentValues.SetValues(item.OriginalValues);
                        break;
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
