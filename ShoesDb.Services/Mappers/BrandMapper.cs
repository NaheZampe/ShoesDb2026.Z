using ShoesDb2026.Entities;
using ShoesDb2026.Services.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Mappers
{
    public static class BrandMapper
    {
        public static BrandListDto ToBrandListDto(Brand brand)
        {
            return new BrandListDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Active = brand.Active
            };
        }
        public static BrandEditDto ToBrandEditDto(Brand brand)
        {
            return new BrandEditDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Active = brand.Active
            };
        }
        public static BrandDetailsDto ToBrandDetailsDto(Brand brand)
        {
            return new BrandDetailsDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Active = brand.Active
            };
        } 
        public static Brand ToEntity(BrandCreateDto brandDto)
        {
            return new Brand
            {
                Name = brandDto.Name,
                Active = brandDto.Active
            };
        }
         public static Brand ToEntity(BrandEditDto brandDto)
        {
            return new Brand
            {
                BrandId = brandDto.BrandId,
                Name = brandDto.Name,
                Active = brandDto.Active
            };
        }
    }
}
