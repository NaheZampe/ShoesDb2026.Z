using ShoesDb2026.Data.Interfaces;

namespace ShoesDb2026.Data
{
    public interface IUnitOfWork
    {
        public IBrandRepository Brands { get; }
        public IGenreRepository Genres { get; }
        public ISportRepository Sports { get; }
        public IShoeRepository Shoes { get; }
        public ISizeRepository Sizes { get; }
        void RollBack();
        void Save();
    }
}
