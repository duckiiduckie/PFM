using AutoMapper;
using IncomeAPI.Models;
using IncomeAPI.Models.Dto;

namespace IncomeAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Income, CreateIncomeDto>().ReverseMap();
                cfg.CreateMap<Category, CreateCategoryDto>().ReverseMap();
                cfg.CreateMap<Category, ReadCategoryDto>().ReverseMap();
                cfg.CreateMap<Income, ReadIncomeDto>().ReverseMap();
            });
        }
    }
}
