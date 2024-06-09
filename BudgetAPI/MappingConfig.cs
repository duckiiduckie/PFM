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
                cfg.CreateMap<Budget, ReadBudget>().ReverseMap();
                cfg.CreateMap<Budget, CreateBudget>().ReverseMap();
            });
        }
    }
}
