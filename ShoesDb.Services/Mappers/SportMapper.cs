using ShoesDb2026.Entities;
using ShoesDb2026.Services.DTOs.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Services.Mappers
{
    public static class SportMapper
    {
        public static SportListDto ToSportListDto(Sport sport)
        {
            return new SportListDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName,
                Active = sport.Active
            };
        }
        public static SportEditDto ToSportEditDto(Sport sport)
        {
            return new SportEditDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName,
                Active = sport.Active
            };
        }
        public static SportDetailsDto ToSportDetailsDto(Sport sport)
        {
            return new SportDetailsDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName,
                Active = sport.Active
            };
        }
        public static Sport ToEntity(SportCreateDto sportDto)
        {
            return new Sport
            {
                SportName = sportDto.SportName,
                Active = sportDto.Active
            };
        }
        public static Sport ToEntity(SportEditDto sportDto)
        {
            return new Sport
            {
                SportId = sportDto.SportId,
                SportName = sportDto.SportName,
                Active = sportDto.Active
            };
        }
        

    }
}
