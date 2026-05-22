
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IGenreRepository
    {
        List<Genre> GetAll();
        IQueryable<Genre> Query();
        Genre? GetById(int id);
        void Delete(int id);
        void Update(Genre genre);
        void Add(Genre genre);
        bool ExistSameName(string name, int? genreId = null);
        bool HasShoes(int genreId);
    }
}
