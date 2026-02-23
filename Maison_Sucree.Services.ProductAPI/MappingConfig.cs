using AutoMapper;
using Maison_Sucree.Services.ProductAPI.Models;
using Maison_Sucree.Services.ProductAPI.Models.Dto;


namespace Maison_Sucree.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            // daca am un coupon Dto, ar trebui sa fie capabil sa converteasca acel coupon si vice versa
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
