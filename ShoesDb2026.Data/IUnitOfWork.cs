using ShoesDb2026.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data
{
    public interface IUnitOfWork
    {
        ISizeRepository Sizes { get; }
        IBrandRepository Brands { get; }
        IGenreRepository Genres { get; }
        ISportRepository Sports { get; }
        ISportShoeRepository SportShoe { get; }
        void Save();
    }
}
