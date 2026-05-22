using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Sport;

namespace ShoesDb2026.Services.Interfaces
{
    public interface ISportService
    {
        Result<List<SportListDto>> GetAll();
        Result<SportListDto> GetById(int id);
        Result<SportEditDto> GetForUpdate(int id);

        Result<SportDetailsDto> GetSportDetails(int id);
        Result Add(SportCreateDto sportDto);
        Result Update(SportEditDto sportDto);
        Result Delete(int id);
    }
}
