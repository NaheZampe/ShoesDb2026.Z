using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Sport
{
    public class SportDetailsDto
    {
        public int SportId { get; set; }
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
        //public List<ShoesListDto> Shoes { get; set; } = new List<ShoesListDto>();
    }
}
