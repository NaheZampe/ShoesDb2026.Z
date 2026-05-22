using ShoesDb2026.Entities;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISportShoeRepository
    {
        List<SportShoe> GetAll();
        SportShoe? GetById(int id);
        IQueryable<Genre> Query();
        void Delete(int id);
        void Update(SportShoe sportShoe);
        void Add(SportShoe sportShoe);
        bool ExistSameName(string model, int? sportShoeId = null);
    }
}
