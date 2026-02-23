using AutoMapper;
using Maison_Sucree.Services.CouponAPI.Models;
using Maison_Sucree.Services.CouponAPI.Models.Dto;

namespace Maison_Sucree.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            // daca am un coupon Dto, ar trebui sa fie capabil sa converteasca acel coupon si vice versa
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
