using Maison_Sucree.Services.ShoppingCartAPI.Models.Dto;

namespace Maison_Sucree.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
