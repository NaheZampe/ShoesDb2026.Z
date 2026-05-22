using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Sport
{
    public class SportCreateDto
    {
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
    }
}
