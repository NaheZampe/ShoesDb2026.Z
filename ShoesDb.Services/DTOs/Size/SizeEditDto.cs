using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Size
{
    public class SizeEditDto
    {
        public int SizeId { get; set; }
        public decimal Number { get; set; }
        public bool Active { get; set; }
    }
}
