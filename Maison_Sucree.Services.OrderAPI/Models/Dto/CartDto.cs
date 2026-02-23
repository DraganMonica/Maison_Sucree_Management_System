using Maison_Sucree.Services.OrderAPI.Models.Dto;

namespace Maison_Sucree.Services.OrderAPI
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
