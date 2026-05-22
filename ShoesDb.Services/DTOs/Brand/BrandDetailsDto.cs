using ShoesDb2026.Services.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Brand
{
    public class BrandDetailsDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public List<ShoesListDto> Shoes { get; set; } = new List<ShoesListDto>();
    }
}
