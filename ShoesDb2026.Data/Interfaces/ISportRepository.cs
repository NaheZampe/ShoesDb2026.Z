using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISportRepository
    {
        List<Sport> GetAll();
        IQueryable<Sport> Query();
        Sport? GetById(int id);
        void Delete(int id);
        void Update(Sport sport);
        void Add(Sport sport);
        bool ExistSameName(string name, int? sportId = null);
        bool HasShoes(int sportId);
    }
}
