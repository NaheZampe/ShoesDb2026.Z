using FluentValidation;
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
            if (_unitOfWork.Sports.ExistSameName(shoe.Model, shoe.ShoeId))
            {
                return Result.Failure("Shoe already exists!!!");
            }
            try
            {
                _unitOfWork.SportShoe.Add(shoe);
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
            var result = _unitOfWork.SportShoe.GetById(id);
            if (result == null)
            {
                return Result.Failure("Shoe not found!!!");
            }

            try
            {
                _unitOfWork.SportShoe.Delete(id);
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
            var shoe = _unitOfWork.SportShoe.GetAll().Select(s => new ShoesListDto
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
            var shoe = _unitOfWork.SportShoe.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesDetailsDto>.Failure("Shoe not found");
            }
            return Result<ShoesDetailsDto>.Success(ShoeMapper.ToShoesDetailsDto(shoe));
        }

        public Result<ShoesListDto> GetById(int id)
        {
            var shoe = _unitOfWork.SportShoe.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesListDto>.Failure("Shoe not found!!!");
            }
            return Result<ShoesListDto>.Success(ShoeMapper.ToShoeListDto(shoe));
        }

        public Result<ShoesEditDto> GetForUpdate(int id)
        {
            var shoe = _unitOfWork.SportShoe.GetById(id);
            if (shoe is null)
            {
                return Result<ShoesEditDto>.Failure("Shoe not found!!!");
            }
            return Result<ShoesEditDto>.Success(ShoeMapper.ToShoeEditDto(shoe));
        }

        public Result Update(ShoesEditDto shoeDto)
        {
            var shoeToValidate = ShoeMapper.ToSportShoe(shoeDto);
            var result = _validator.Validate(shoeToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var shoeToDb = _unitOfWork.SportShoe.GetById(shoeToValidate.ShoeId);
            if (shoeToDb is null)
            {
                return Result.Failure("Sport not found!!!");
            }
            shoeToDb.Model = shoeToValidate.Model;
            shoeToDb.Price = shoeToValidate.Price;
            shoeToDb.Description = shoeToValidate.Description;
            shoeToDb.Active = shoeToValidate.Active;
            shoeToDb.BrandId = shoeToValidate.BrandId;
            shoeToDb.SizeId = shoeToValidate.SizeId;
            shoeToDb.SportId = shoeToValidate.SportId;
            shoeToDb.GenreId = shoeToValidate.GenreId;

            if (_unitOfWork.SportShoe.ExistSameName(shoeToValidate.Model, shoeToValidate.ShoeId))
            {
                return Result.Failure("Shoe already exists!!!");
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
