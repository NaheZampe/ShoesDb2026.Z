using ShoesDb2026.Entities;
using ShoesDb2026.Services.DTOs.Size;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShoesDb2026.Services.Mappers
{
    public static class SizeMapper
    {
        public static SizeListDto ToSizeListDto(SiZe size)
        {
            return new SizeListDto
            {
                SizeId = size.SizeId,
                Number = size.SizeNumber,
                Active = size.Active
            };
        }
        public static SizeEditDto ToSizeEditDto(SiZe size)
        {
            return new SizeEditDto
            {
                SizeId = size.SizeId,
                Number = size.SizeNumber,
                Active = size.Active
            };
        }
        public static SiZe ToSize(SizeEditDto sizeEditDto)
        {
            return new SiZe
            {
                SizeId = sizeEditDto.SizeId,
                SizeNumber = sizeEditDto.Number,
                Active = sizeEditDto.Active
            };
        }
    }
}
