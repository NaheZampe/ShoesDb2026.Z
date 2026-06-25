using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        bool Exist(Genre genre);
        bool IsRelated(Genre genre);
    }
}
