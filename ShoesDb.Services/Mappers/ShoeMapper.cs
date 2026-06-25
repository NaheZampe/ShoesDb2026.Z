using ShoesDb2026.Entities;
using ShoesDb2026.Services.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Mappers
{
    public static class ShoeMapper
    {
        public static ShoesEditDto ToShoeEditDto(SportShoe shoe)
        {
            return new ShoesEditDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                Description = shoe.Description,
                Active = shoe.Active,
                BrandId = shoe.BrandId,
                SizeId = shoe.SizeId,
                SportId = shoe.SportId,
                GenreId = shoe.GenreId,
                RowVersion = shoe.RowVersion
            };
        }
        public static ShoesDetailsDto ToShoesDetailsDto(SportShoe shoe)
        {
            return new ShoesDetailsDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                Description = shoe.Description,
                Active = shoe.Active,
                BrandName = shoe.Brand != null ? shoe.Brand.Name : string.Empty,
                Size = shoe.Size != null ? shoe.Size.SizeNumber : 0,
                SportName = shoe.Sport != null ? shoe.Sport.SportName : string.Empty,
                GenreName = shoe.Genre != null ? shoe.Genre.GenreName : string.Empty
            };
        }
        public static SportShoe ToSportShoe(ShoesCreateDto shoeCreateDto)
        {
            return new SportShoe
            {
                Model = shoeCreateDto.Model,
                Price = shoeCreateDto.Price,
                Description = shoeCreateDto.Description,
                Active = shoeCreateDto.Active,
                BrandId = shoeCreateDto.BrandId,
                SizeId = shoeCreateDto.SizeId,
                SportId = shoeCreateDto.SportId,
                GenreId = shoeCreateDto.GenreId
            };
        }
        public static SportShoe ToSportShoe(ShoesEditDto shoeEditDto)
        {
            return new SportShoe
            {
                ShoeId = shoeEditDto.ShoeId,
                Model = shoeEditDto.Model,
                Price = shoeEditDto.Price,
                Description = shoeEditDto.Description,
                Active = shoeEditDto.Active,
                BrandId = shoeEditDto.BrandId,
                SizeId = shoeEditDto.SizeId,
                SportId = shoeEditDto.SportId,
                GenreId = shoeEditDto.GenreId,
                RowVersion = shoeEditDto.RowVersion
            };
        }
        public static ShoesListDto ToShoeListDto(SportShoe shoe)
        {
            return new ShoesListDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                Active = shoe.Active,
                BrandName = shoe.Brand != null ? shoe.Brand.Name : string.Empty
            };
        }
    }
}
