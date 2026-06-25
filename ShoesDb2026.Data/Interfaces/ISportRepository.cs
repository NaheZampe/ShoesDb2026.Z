using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISportRepository : IConcurrentRepository<Sport>
    {
        bool Exist(Sport sport);
        bool IsRelated(Sport sport);
    }
}
