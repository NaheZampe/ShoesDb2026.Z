using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Genre
{
    public class GenreCreateDto
    {
        public string GenreName { get; set; } = null!;
        public bool Active { get; set; }
    }
}
