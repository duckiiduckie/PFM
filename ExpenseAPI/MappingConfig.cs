using AutoMapper;
using ExpenseAPI.Models;
using ExpenseAPI.Models.Dto;

namespace ExpenseAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Expense, CreateExpenseDto>().ReverseMap();
                cfg.CreateMap<Category, CreateCategoryDto>().ReverseMap();
                cfg.CreateMap<Category, ReadCategoryDto>().ReverseMap();
                cfg.CreateMap<Expense, ReadExpenseDto>().ReverseMap();
            });
        }
    }
}
