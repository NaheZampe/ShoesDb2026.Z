using ShoesDb2026.Services.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Genre
{
    public class GenreDetailsDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;
        public bool Active { get; set; }
        public List<ShoesListDto> Shoes { get; set; } = new List<ShoesListDto>();
    }
}
