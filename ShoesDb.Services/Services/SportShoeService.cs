using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Shoe;
using ShoesDb2026.Services.DTOs.Sport;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Services
{
    public class SportShoeService : ISportShoeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SportShoe> _validator;

        public SportShoeService(IUnitOfWork unitOfWork, IValidator<SportShoe> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Add(ShoesCreateDto shoeDto)
        {
            var shoe = ShoeMapper.ToSportShoe(shoeDto);
            var result = _validator.Validate(shoe);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.Shoes.Exist(shoe))
            {
                return Result.Failure("Shoe already exists!!!");
            }
            try
            {
                _unitOfWork.Shoes.Add(shoe);
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
            var result = _unitOfWork.Shoes.GetById(id);
            if (result == null)
            {
                return Result.Failure("Shoe not found!!!");
            }

            try
            {
                _unitOfWork.Shoes.Delete(id);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<ShoesListDto>> GetAll()
        {
            var shoe = _unitOfWork.Shoes.GetAll().Select(s => new ShoesListDto
            {
                Model = s.Model,
                ShoeId = s.ShoeId,
                Price = s.Price,
                Active = s.Active,
                BrandName=s.Brand != null ? s.Brand.Name : string.Empty,

            }).ToList();
            return Result<List<ShoesListDto>>.Success(shoe);
        }

        public Result<ShoesDetailsDto> GetShoeDetails(int id)
        {
            var shoe = _unitOfWork.Shoes.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesDetailsDto>.Failure("Shoe not found");
            }
            return Result<ShoesDetailsDto>.Success(ShoeMapper.ToShoesDetailsDto(shoe));
        }

        public Result<ShoesListDto> GetById(int id)
        {
            var shoe = _unitOfWork.Shoes.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesListDto>.Failure("Shoe not found!!!");
            }
            return Result<ShoesListDto>.Success(ShoeMapper.ToShoeListDto(shoe));
        }

        public Result<ShoesEditDto> GetForUpdate(int id)
        {
            var shoe = _unitOfWork.Shoes.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesEditDto>.Failure("Shoe not found!!!");
            }
            return Result<ShoesEditDto>.Success(ShoeMapper.ToShoeEditDto(shoe));
        }

        public Result Update(ShoesEditDto shoeDto)
        {
            try
            {

                var entidad = ShoeMapper.ToSportShoe(shoeDto);

                var validationResult = _validator.Validate(entidad);

                if (!validationResult.IsValid)
                {
                    return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage)
                            .ToList());
                }

                if (_unitOfWork.Shoes.Exist(entidad))
                {
                    return Result.Failure(
                        $" Ya existe un calzado con el nombre {entidad.Model}");
                }

                _unitOfWork.Shoes.Update(entidad, shoeDto.ShoeId, shoeDto.RowVersion);

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
                    $"Calzado con ID {shoeDto.ShoeId} no encontrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Error al intentar editar el calzado: {ex.Message}");
            }
        }
    }
}
