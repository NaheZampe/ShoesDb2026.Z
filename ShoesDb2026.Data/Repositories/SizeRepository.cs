using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SizeRepository : ConcurrentRepository<SiZe>, ISizeRepository
    {
        public SizeRepository(ShoesDbContext context) : base(context)
        {
        }

        public bool Exist(SiZe size)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(SiZe size)
        {
            throw new NotImplementedException();
        }
    }
}
