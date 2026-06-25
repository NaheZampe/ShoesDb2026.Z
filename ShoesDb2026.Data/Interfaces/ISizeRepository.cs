using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISizeRepository : IConcurrentRepository<SiZe>
    {
        bool Exist(SiZe size);
        bool IsRelated(SiZe size);
    }
}
