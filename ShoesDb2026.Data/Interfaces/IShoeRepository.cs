using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IShoeRepository : IConcurrentRepository<SportShoe>
    {
        bool Exist(SportShoe shoe);
        bool IsRelated(SportShoe shoe);
    }
}
