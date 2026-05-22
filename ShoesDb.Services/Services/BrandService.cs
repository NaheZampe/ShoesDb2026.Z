using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Common;
using ShoesDb2026.Services.DTOs.Brand;
using ShoesDb2026.Services.DTOs.Shoe;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Brand> _validator;

        public BrandService(IUnitOfWork unitOfWork, IValidator<Brand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Add(BrandCreateDto brandDto)
        {
            var brand= BrandMapper.ToEntity(brandDto);
            var result = _validator.Validate(brand);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.Brands.ExistSameName(brand.Name, brand.BrandId))
            {
                return Result.Failure("Brand already exists!!!");
            }
            try
            {
                _unitOfWork.Brands.Add(brand);
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
            var result = _unitOfWork.Brands.GetById(id);
            if (result == null)
            {
                return Result.Failure("Brand not found!!!");
            }
            if (_unitOfWork.Brands.HasShoes(id))
            {
                return Result.Failure("Brand has associated Shoes");
            }

            try
            {
                _unitOfWork.Brands.Delete(id);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<BrandListDto>> GetAll()
        {
            var brands = _unitOfWork.Brands.GetAll().Select(b => new BrandListDto 
            {   
                BrandId = b.BrandId,
                Name = b.Name,
                Active = b.Active}).ToList();
            return Result<List<BrandListDto>>.Success(brands);
        }

        public Result<BrandDetailsDto> GetBrandDetails(int id)
        {
            var query = _unitOfWork.Brands.Query()
                .Where(b => b.BrandId == id)
                .Select(b => new BrandDetailsDto
                {
                    BrandId = b.BrandId,
                    Name = b.Name,
                    Active = b.Active,
                    Shoes = b.Shoes!.Select(s => new ShoesListDto
                    {
                        ShoeId = s.ShoeId,
                        Model = s.Model,
                        Price = s.Price
                    }).ToList()
                }).FirstOrDefault();
            if (query == null)
            {
                return Result<BrandDetailsDto>.Failure("Brand not found");
            }
            return Result<BrandDetailsDto>.Success(query);
        }

        //public Result<BrandListDto> GetById(int id)
        //{
        //                        Title = b.Title,
        //                        PublisherName = b.Publisher.Name,
        //                        Price = b.Price,
        //                        Stock = b.Stock,
        //                    }).ToList()
        //        }).FirstOrDefault();
        //    if (query == null)
        //    {
        //        return Result<AuthorDetailsDto>.Failure("Author not found");
        //    }
        //    return Result<AuthorDetailsDto>.Success(query);
        //}

        public Result<BrandListDto> GetById(int id)
        {
            var brand = _unitOfWork.Brands.GetById(id);
            if (brand is null)
            {
                return Result<BrandListDto>.Failure("Brand not found!!!");
            }
            return Result<BrandListDto>.Success(BrandMapper.ToBrandListDto(brand));
        }

        public Result<BrandEditDto> GetForUpdate(int id)
        {
            var brand = _unitOfWork.Brands.GetById(id);
            if (brand is null)
            {
                return Result<BrandEditDto>.Failure("Brand not found!!!");
            }
            return Result<BrandEditDto>.Success(BrandMapper.ToBrandEditDto(brand));
        }

        public Result Update(BrandEditDto brandDto)
        {
            var brandToValidate = BrandMapper.ToEntity(brandDto);
            var result = _validator.Validate(brandToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var brandFromDb = _unitOfWork.Brands.GetById(brandToValidate.BrandId);
            if (brandFromDb is null)
            {
                return Result.Failure("Brand not found!!!");
            }
            brandFromDb.Name = brandDto.Name;
            brandFromDb.Active = brandDto.Active;
            if (_unitOfWork.Brands.ExistSameName(brandToValidate.Name, brandToValidate.BrandId))
            {
                return Result.Failure("Brand already exists!!!");
            }
            try
            {
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return  Result.Failure(ex.Message);
            }
        }
    }
}
