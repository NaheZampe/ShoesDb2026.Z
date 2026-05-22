using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Shoe
{
    public class ShoesDetailsDto
    {
        public int ShoesId { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Active { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int SportId { get; set; }
        public int GenreId { get; set; }
        public string Description { get; internal set; }
    }
}
