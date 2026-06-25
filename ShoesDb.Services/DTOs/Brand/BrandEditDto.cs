using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Brand
{
    public class BrandEditDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
