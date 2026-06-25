using ShoesDb2026.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class Brand : IConcurrencyEntity
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public ICollection<SportShoe>? Shoes { get; set; }
        public byte[] RowVersion {  get; set; } = null!;
    }
}
