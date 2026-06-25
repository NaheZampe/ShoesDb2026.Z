using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;

namespace ShoesDb2026.Data.Repositories
{
    public class ShoeRepository : ConcurrentRepository<SportShoe>, IShoeRepository
    {
        public ShoeRepository(ShoesDbContext context) : base(context)
        {
        }

        public bool Exist(SportShoe shoe)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(SportShoe shoe)
        {
            throw new NotImplementedException();
        }
    }
}
