using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class SiZe
    {
        public int SizeId { get; set; }
        public decimal SizeNumber { get; set; }
        public bool Active { get; set; }
        public ICollection<SportShoe>? Shoes { get; set; }
    }
}
