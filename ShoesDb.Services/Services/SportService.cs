using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
            if (_unitOfWork.Sports.Exist(sport))
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
            if (_unitOfWork.Sports.IsRelated(result))
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
            try
            {
                var entidad = SportMapper.ToEntity(sportDto);

                var validationResult = _validator.Validate(entidad);

                if (!validationResult.IsValid)
                {
                    return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage)
                            .ToList());
                }

                if (_unitOfWork.Sports.Exist(entidad))
                {
                    return Result.Failure(
                        $" Ya existe un deporte con el nombre {entidad.SportName}");
                }

                _unitOfWork.Sports.Update(entidad, sportDto.SportId, sportDto.RowVersion);

                _unitOfWork.Save();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)//acá decía DBConcurrencyException!!!
            {
                _unitOfWork.RollBack();

                return Result.ConcurrencyFailure(
                    "Otro usuario modificó el registro.\nLa grilla se recargará automáticamente");
            }
            catch (KeyNotFoundException)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Deporte con ID {sportDto.SportId} no encontrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Error al intentar editar el deporte: {ex.Message}");
            }
        }
    }
}
