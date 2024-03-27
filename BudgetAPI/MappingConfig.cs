using AutoMapper;
using BudgetAPI.Models;
using BudgetAPI.Models.Dto;

namespace BudgetAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Budget, ReadBudgetDto>().ReverseMap();
                cfg.CreateMap<Budget, CreateBudgetDto>().ReverseMap();
                cfg.CreateMap<Category, CreateCategoryDto>().ReverseMap();
                cfg.CreateMap<Category, ReadCategoryDto>().ReverseMap();
            });
        }
    }
}
