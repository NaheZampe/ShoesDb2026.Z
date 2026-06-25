using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.DTOs.Sport
{
    public class SportEditDto
    {
        public int SportId { get; set; }
        public string SportName { get; set; } = null!;
        public bool Active { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
