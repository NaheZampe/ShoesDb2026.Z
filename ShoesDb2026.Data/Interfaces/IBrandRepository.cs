using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        IQueryable<Brand> Query();
        Brand? GetById(int id);
        void Delete(int id);
        void Update(Brand brand);
        void Add(Brand brand);
        bool ExistSameName(string name, int? brandId = null);
        bool HasShoes(int brandId);
    }
}
