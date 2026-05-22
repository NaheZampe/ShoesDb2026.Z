using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class SportShoe
    {
        [Key]
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public bool Active { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int SizeId { get; set; }
        public SiZe? Size { get; set; }
        public int SportId { get; set; }
        public Sport? Sport { get; set; }
        public Genre? Genre { get; set; }
        public int GenreId { get; set; }
    }
}
