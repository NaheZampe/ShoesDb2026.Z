using ShoesDb2026.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoesDbContext _context;

        public UnitOfWork(ShoesDbContext context, ISizeRepository sizeRepository,
            IBrandRepository brandRepository, IGenreRepository genreRepository,
            ISportRepository sportRepository, ISportShoeRepository sportShoe)
        {
            _context = context;
            Sizes = sizeRepository;
            Brands = brandRepository;
            Genres = genreRepository;
            Sports = sportRepository;
            SportShoe = sportShoe;
        }
        public IGenreRepository Genres { get; }
        public ISportRepository Sports { get; }
        public ISizeRepository Sizes { get; }
        public IBrandRepository Brands {  get; }
        public ISportShoeRepository SportShoe {  get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
