using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
