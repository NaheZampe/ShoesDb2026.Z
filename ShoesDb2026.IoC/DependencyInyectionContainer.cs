using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Data;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Data.Repositories;
using ShoesDb2026.Entities;
using ShoesDb2026.Services.Interfaces;
using ShoesDb2026.Services.Services;
using ShoesDb2026.Services.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.IoC
{
    public static class DependencyInyectionContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ShoesDbContext>();

            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IValidator<SiZe>, SizeValidator>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IValidator<Brand>, BrandValidator>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IValidator<Genre>, GenreValidator>();

            services.AddScoped<ISportRepository, SportRepository>();
            services.AddScoped<ISportService, SportService>();
            services.AddScoped<IValidator<Sport>, SportValidator>();

            services.AddScoped<ISportShoeRepository, SportShoeRepository>();
            services.AddScoped<ISportShoeService, SportShoeService>();
            services.AddScoped<IValidator<SportShoe>, ShoeValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services.BuildServiceProvider();
        }
    }
}
