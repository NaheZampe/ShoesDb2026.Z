using FluentValidation;
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
            var sizeToValidate = SizeMapper.ToSize(editDto);
            var result = _validator.Validate(sizeToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            SiZe? size = _unitOfWork.Sizes.GetById(editDto.SizeId);
            if (size==null)
            {
                return Result.Failure("Size not found");
            }
            size.SizeNumber = editDto.Number;
            if (_unitOfWork.Sizes.Exist(editDto.Number,editDto.SizeId))
            {
                return Result.Failure("Size already exist!!!");
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
