using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SportRepository : ISportRepository
    {
        private readonly ShoesDbContext _context;

        public SportRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public void Add(Sport sport)
        {
            _context.Sports.Add(sport);
        }

        public void Delete(int id)
        {
            var sport = _context.Sports.Find(id);
            if (sport == null)
            {
                return;
            }
            sport.Active = false;
        }

        public bool ExistSameName(string name, int? sportId = null)
        {
            return _context.Sports.Any(s => s.SportName == name && s.SportId != sportId);
        }

        public List<Sport> GetAll()
        {
            return _context.Sports.AsNoTracking().ToList();
        }

        public Sport? GetById(int id)
        {
            return _context.Sports.Find(id);
        }

        public bool HasShoes(int sportId)
        {
            return _context.Shoes.Any(s => s.SportId == sportId);
        }

        public IQueryable<Sport> Query()
        {
            return _context.Sports.AsNoTracking().AsQueryable();
        }

        public void Update(Sport sport)
        {
            _context.Sports.Update(sport);
        }
    }
}
