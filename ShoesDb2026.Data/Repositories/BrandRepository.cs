using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class BrandRepository : ConcurrentRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ShoesDbContext context) : base(context)
        {
        }

        public bool Exist(Brand brand)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
