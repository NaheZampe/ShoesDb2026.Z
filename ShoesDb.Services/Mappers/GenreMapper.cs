using ShoesDb2026.Entities;
using ShoesDb2026.Services.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Mappers
{
    public static class GenreMapper
    {
        public static GenreListDto ToGenreListDto(Genre genre)
        {
            return new GenreListDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Active = genre.Active
            };
        }
        public static GenreDetailsDto ToGenreDetailsDto(Genre genre)
        {
            return new GenreDetailsDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Active = genre.Active
            };
        }
        public static GenreEditDto ToGenreEditDto(Genre genre)
        {
            return new GenreEditDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Active = genre.Active
            };
        }
        public static Genre ToEntity(GenreEditDto genreEditDto)
        {
            return new Genre
            {
                GenreId = genreEditDto.GenreId,
                GenreName = genreEditDto.GenreName,
                Active = genreEditDto.Active
            };
        }
        public static Genre ToEntity(GenreCreateDto genreCreateDto)
        {
            return new Genre
            {
                GenreName = genreCreateDto.GenreName,
                Active = genreCreateDto.Active
            };
        }
    }
}
