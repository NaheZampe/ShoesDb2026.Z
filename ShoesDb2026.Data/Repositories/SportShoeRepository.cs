using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SportShoeRepository : ISportShoeRepository
    {
        private readonly ShoesDbContext _context;

        public SportShoeRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public void Add(SportShoe sportShoe)
        {
            _context.Shoes.Add(sportShoe);
        }

        public void Delete(int id)
        {
            var sportShoe = _context.Shoes.Find(id);
            if (sportShoe == null)
            {
                return; 
            }
            sportShoe.Active = false;
        }

        public bool ExistSameName(string model, int? sportShoeId = null)
        {
            return _context.Shoes.Any(s => s.Model == model && s.ShoeId != sportShoeId);
        }

        public List<SportShoe> GetAll()
        {
            return _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .AsNoTracking()
                .ToList();
        }

        public SportShoe? GetById(int id)
        {
            return _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .FirstOrDefault(s => s.ShoeId == id);
        
        }

        public IQueryable<Genre> Query()
        {
            return _context.Genres.AsNoTracking().AsQueryable();
        }

        public void Update(SportShoe sportShoe)
        {
            _context.Shoes.Update(sportShoe);
        }
    }
}
