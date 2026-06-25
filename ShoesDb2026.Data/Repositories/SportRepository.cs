using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SportRepository : ConcurrentRepository<Sport>, ISportRepository
    {
        public SportRepository(ShoesDbContext context) : base(context)
        {
        }

        public bool Exist(Sport sport)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(Sport sport)
        {
            throw new NotImplementedException();
        }
    }
}
