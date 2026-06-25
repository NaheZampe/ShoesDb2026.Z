using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ShoesDbContext context) : base(context)
        {
        }

        public bool Exist(Genre genre)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
