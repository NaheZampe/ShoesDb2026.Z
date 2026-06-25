using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Size;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Services
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SiZe> _validator;

        public SizeService(IUnitOfWork unitOfWork, IValidator<SiZe> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result<List<SizeListDto>> GetAllSizes()
        {
            var sizes = _unitOfWork.Sizes.GetAll().Select(s => new SizeListDto
            {
                SizeId=s.SizeId,
                Number = s.SizeNumber,
                Active = s.Active
            }).ToList();
            return Result<List<SizeListDto>>.Success(sizes);
        }

        public Result<SizeEditDto> GetById(int id)
        {
            var size = _unitOfWork.Sizes.GetById(id);
            if (size == null) return Result<SizeEditDto>.Failure("Size not found");
            return Result<SizeEditDto>.Success(SizeMapper.ToSizeEditDto(size));
        }

        public Result<SizeEditDto> GetForUpdate(int id)
        {
            var size = _unitOfWork.Sizes.GetById(id);
            if (size == null) return Result<SizeEditDto>.Failure("Size not found");
            return Result<SizeEditDto>.Success(SizeMapper.ToSizeEditDto(size));
        }

        public Result Update(SizeEditDto editDto)
        {
            try
            {
                var entidad = SizeMapper.ToEntity(editDto);

                var validationResult = _validator.Validate(entidad);

                if (!validationResult.IsValid)
                {
                    return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage)
                            .ToList());
                }

                if (_unitOfWork.Sizes.Exist(entidad))
                {
                    return Result.Failure(
                        $" Ya existe un talle {entidad.SizeNumber}");
                }

                _unitOfWork.Sizes.Update(entidad, editDto.SizeId, editDto.RowVersion);

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
                    $"Tamaño con ID {editDto.SizeId} no encontrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Error al intentar editar el tamaño: {ex.Message}");
            }
        }
    }
}
