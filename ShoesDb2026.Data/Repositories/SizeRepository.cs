using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ShoesDbContext _context;

        public SizeRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public bool Exist(decimal number, int? sizeId=null)
        {
            return _context.Sizes.Any(s=>s.SizeNumber==number && s.SizeId!=sizeId);
        }

        public List<SiZe> GetAll()
        {
            return _context.Sizes.AsNoTracking().ToList();
        }

        public SiZe? GetById(int id)
        {
            return _context.Sizes.Find(id);
        }

        public void Update(SiZe size)
        {
            _context.Sizes.Update(size);
        }
    }
}
