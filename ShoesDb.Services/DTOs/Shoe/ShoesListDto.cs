using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Shoe
{
    public class ShoesListDto
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public string BrandName { get; set; } = null!;
        
    }
}
