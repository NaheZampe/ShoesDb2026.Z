using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Brand;
using ShoesDb2026.Services.DTOs.Genre;
using ShoesDb2026.Services.DTOs.Shoe;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Mappers;

namespace ShoesDb2026.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Genre> _validator;

        public GenreService(IUnitOfWork unitOfWork, IValidator<Genre> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Add(GenreCreateDto genreDto)
        {
            var genre = GenreMapper.ToEntity(genreDto);
            var result = _validator.Validate(genre);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.Genres.ExistSameName(genre.GenreName, genre.GenreId))
            {
                return Result.Failure("Genre already exists!!!");
            }
            try
            {
                _unitOfWork.Genres.Add(genre);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result Delete(int id)
        {
            var result = _unitOfWork.Genres.GetById(id);
            if (result == null)
            {
                return Result.Failure("Genre not found!!!");
            }
            if (_unitOfWork.Genres.HasShoes(id))
            {
                return Result.Failure("Genre has associated Shoes");
            }

            try
            {
                _unitOfWork.Genres.Delete(id);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<GenreListDto>> GetAll()
        {
            var genres = _unitOfWork.Genres.GetAll().Select(g => new GenreListDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName,
                Active = g.Active
            }).ToList();
            return Result<List<GenreListDto>>.Success(genres);
        }

        public Result<GenreListDto> GetById(int id)
        {
            var genre = _unitOfWork.Genres.GetById(id);
            if (genre is null)
            {
                return Result<GenreListDto>.Failure("Genre not found!!!");
            }
            return Result<GenreListDto>.Success(GenreMapper.ToGenreListDto(genre));
        }

        public Result<GenreEditDto> GetForUpdate(int id)
        {
            var genre = _unitOfWork.Genres.GetById(id);
            if (genre is null)
            {
                return Result<GenreEditDto>.Failure("Genre not found!!!");
            }
            return Result<GenreEditDto>.Success(GenreMapper.ToGenreEditDto(genre));
        }

        public Result<GenreDetailsDto> GetGenreDetails(int id)
        {
            var query = _unitOfWork.Genres.Query()
               .Where(g => g.GenreId == id)
               .Select(g => new GenreDetailsDto
               {
                   GenreId = g.GenreId,
                   GenreName = g.GenreName,
                   Active = g.Active,
                   Shoes = g.Shoes!.Select(s => new ShoesListDto
                   {
                       ShoeId = s.ShoeId,
                       Model = s.Model,
                       Price = s.Price
                   }).ToList()
               }).FirstOrDefault();
            if (query == null)
            {
                return Result<GenreDetailsDto>.Failure("Genre not found");
            }
            return Result<GenreDetailsDto>.Success(query);
        }

        public Result Update(GenreEditDto genreDto)
        {
            var genreToValidate = GenreMapper.ToEntity(genreDto);
            var result = _validator.Validate(genreToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var genreFromDb = _unitOfWork.Genres.GetById(genreToValidate.GenreId);
            if (genreFromDb is null)
            {
                return Result.Failure("Genre not found!!!");
            }
            genreFromDb.GenreName = genreDto.GenreName;
            genreFromDb.Active = genreDto.Active;
            if (_unitOfWork.Genres.ExistSameName(genreToValidate.GenreName, genreToValidate.GenreId))
            {
                return Result.Failure("Genre already exists!!!");
            }
            try
            {
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
