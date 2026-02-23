using Maison_Sucree.Services.OrderAPI.Models.Dto;

namespace Maison_Sucree.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
