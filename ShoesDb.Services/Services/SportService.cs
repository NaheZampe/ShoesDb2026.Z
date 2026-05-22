using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Brand;
using ShoesDb2026.Services.DTOs.Genre;
using ShoesDb2026.Services.DTOs.Shoe;
using ShoesDb2026.Services.DTOs.Sport;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Services
{
    public class SportService : ISportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Sport> _validator;

        public SportService(IUnitOfWork unitOfWork, IValidator<Sport> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Add(SportCreateDto sportDto)
        {
            var sport = SportMapper.ToEntity(sportDto);
            var result = _validator.Validate(sport);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.Sports.ExistSameName(sport.SportName, sport.SportId))
            {
                return Result.Failure("Sport already exists!!!");
            }
            try
            {
                _unitOfWork.Sports.Add(sport);
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
            var result = _unitOfWork.Sports.GetById(id);
            if (result == null)
            {
                return Result.Failure("Sport not found!!!");
            }
            if (_unitOfWork.Sports.HasShoes(id))
            {
                return Result.Failure("Sport has associated Shoes");
            }

            try
            {
                _unitOfWork.Sports.Delete(id);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<SportListDto>> GetAll()
        {
            var sports = _unitOfWork.Sports.GetAll().Select(s => new SportListDto
            {
                SportId = s.SportId,
                SportName = s.SportName,
                Active = s.Active
            }).ToList();
            return Result<List<SportListDto>>.Success(sports);
        }

        public Result<SportListDto> GetById(int id)
        {
            var sport = _unitOfWork.Sports.GetById(id);
            if (sport is null)
            {
                return Result<SportListDto>.Failure("Sport not found!!!");
            }
            return Result<SportListDto>.Success(SportMapper.ToSportListDto(sport));
        }

        public Result<SportEditDto> GetForUpdate(int id)
        {
            var sport = _unitOfWork.Sports.GetById(id);
            if (sport is null)
            {
                return Result<SportEditDto>.Failure("Sport not found!!!");
            }
            return Result<SportEditDto>.Success(SportMapper.ToSportEditDto(sport));
        }

        public Result<SportDetailsDto> GetSportDetails(int id)
        {
            var query = _unitOfWork.Sports.Query()
               .Where(s => s.SportId == id)
               .Select(s => new SportDetailsDto
               {
                   SportId = s.SportId,
                   SportName = s.SportName,
                   Active = s.Active,
                   Shoes = s.Shoes!.Select(s => new ShoesListDto
                   {
                       ShoeId = s.ShoeId,
                       Model = s.Model,
                       Price = s.Price
                   }).ToList()
               }).FirstOrDefault();
            if (query == null)
            {
                return Result<SportDetailsDto>.Failure("Sport not found");
            }
            return Result<SportDetailsDto>.Success(query);
        }

        public Result Update(SportEditDto sportDto)
        {
            var sportToValidate = SportMapper.ToEntity(sportDto);
            var result = _validator.Validate(sportToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var sportToDb = _unitOfWork.Sports.GetById(sportToValidate.SportId);
            if (sportToDb is null)
            {
                return Result.Failure("Sport not found!!!");
            }
            sportToDb.SportName = sportDto.SportName;
            sportToDb.Active = sportDto.Active;
            if (_unitOfWork.Sports.ExistSameName(sportToValidate.SportName, sportToValidate.SportId))
            {
                return Result.Failure("Sport already exists!!!");
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
