using AutoMapper;
using Maison_Sucree.Services.ShoppingCartAPI.Models;
using Maison_Sucree.Services.ShoppingCartAPI.Models.Dto;


namespace Maison_Sucree.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            // daca am un coupon Dto, ar trebui sa fie capabil sa converteasca acel coupon si vice versa
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
