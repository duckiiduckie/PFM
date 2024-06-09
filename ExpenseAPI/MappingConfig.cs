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
                cfg.CreateMap<FuturePlannedExpense, ReadFuturePlannedExpense>().ReverseMap();
                cfg.CreateMap<DailyExpense, ReadDailyExpense>().ReverseMap();
                cfg.CreateMap<CreateFuturePlannedExpense, FuturePlannedExpense>().ReverseMap();
                cfg.CreateMap<CreateDailyExpense, DailyExpense>().ReverseMap();
            });
        }
    }
}
