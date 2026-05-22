using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Brand;

namespace ShoesDb2026.Services.Interfaces
{
    public interface IBrandService
    {
        Result<List<BrandListDto>> GetAll();
        Result<BrandListDto> GetById(int id);
        Result<BrandEditDto> GetForUpdate(int id);

        Result<BrandDetailsDto> GetBrandDetails(int id);
        Result Add(BrandCreateDto brandDto);
        Result Update(BrandEditDto brandDto);
        Result Delete(int id);
    }
}
