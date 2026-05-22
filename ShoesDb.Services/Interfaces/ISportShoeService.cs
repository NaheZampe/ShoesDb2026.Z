using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Brand;
using ShoesDb2026.Services.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Interfaces
{
    public interface ISportShoeService
    {
        Result<List<ShoesListDto>> GetAll();
        Result<ShoesListDto> GetById(int id);
        Result<ShoesEditDto> GetForUpdate(int id);

        Result<ShoesDetailsDto> GetBrandDetails(int id);
        Result Add(ShoesCreateDto shoeDto);
        Result Update(ShoesEditDto shoeDto);
        Result Delete(int id);
    }
}
