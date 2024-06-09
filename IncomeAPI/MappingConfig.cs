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
                cfg.CreateMap<MainIncome, ReadMainIncome>().ReverseMap();
                cfg.CreateMap<AdditionalIncome, ReadAdditionalIncome>().ReverseMap();
                cfg.CreateMap<CreateMainIncome, MainIncome>().ReverseMap();
                cfg.CreateMap<CreateAdditionalIncome, AdditionalIncome>().ReverseMap();
            });
        }
    }
}
