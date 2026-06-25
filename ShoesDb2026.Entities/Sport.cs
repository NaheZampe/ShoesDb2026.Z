using ShoesDb2026.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class Sport : IConcurrencyEntity
    {
        public int SportId { get; set; }
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
        public ICollection<SportShoe>? Shoes { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
