using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Interfaces
{
    public interface IGenreService
    {
        Result<List<GenreListDto>> GetAll();
        Result<GenreListDto> GetById(int id);
        Result<GenreEditDto> GetForUpdate(int id);

        Result<GenreDetailsDto> GetGenreDetails(int id);
        Result Add(GenreCreateDto genreDto);
        Result Update(GenreEditDto genreDto);
        Result Delete(int id);
    }
}
