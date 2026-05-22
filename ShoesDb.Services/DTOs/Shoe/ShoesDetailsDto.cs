using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Shoe
{
    public class ShoesDetailsDto
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Active { get; set; }
        public string BrandName { get; set; } = null!;
        public decimal Size { get; set; }
        public string SportName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string Description { get; internal set; } = null!;
    }
}
