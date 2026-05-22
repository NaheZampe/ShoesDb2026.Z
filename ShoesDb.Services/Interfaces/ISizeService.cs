using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Size;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Interfaces
{
    public interface ISizeService
    {
        Result<List<SizeListDto>> GetAllSizes();
        Result<SizeEditDto> GetById(int id);
        Result<SizeEditDto> GetForUpdate(int id);
        Result Update(SizeEditDto editDto);
    }
}
