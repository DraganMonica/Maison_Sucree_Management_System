using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Maison_Sucree.Web.Models;

using Maison_Sucree.Web.Utility;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Maison_Sucree.Web.Service.IService;

namespace Maison_Sucree.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {

            return View(await LoadCartDtoBasedOnLoggedInUser());
        }
        
        
        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {

            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            //loading a new copy with all the records that are populated
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;
            

            //I need to call the orderservice- asa ca trebuie sa injectez mai sus orderservice
            var response = await _orderService.CreateOrder(cart);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            
            if (response != null && response.IsSuccess) 
            {
                
                //get stripe session & redirect to stripe to place order
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                
                
                StripeRequestDto stripeRequestDto = new()
                {
                    //ApprovedUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    ApprovedUrl = domain + "cart/Confirmation?orderId=" 
                                 + orderHeaderDto.OrderHeaderId,

                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);

                StripeRequestDto stripeResponseResult =JsonConvert.DeserializeObject<StripeRequestDto>
                                            (Convert.ToString(stripeResponse.Result));

                Response.Headers.Add("Location",stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);
                
            }

            return View();
        }
        
        
        
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto? response = await _orderService.ValidateStripeSession(orderId);

            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));

                // ← GOLEȘTE COȘUL indiferent de status
                var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)
                                 ?.FirstOrDefault()?.Value;
                await _cartService.ClearCartAsync(userId);

                if (orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
            }
            return View(orderId);
        }
        

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();

        }
        
        
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {

            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        
        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {

            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);

            if (response != null && response.IsSuccess)
            {
                CartDto cartDto =JsonConvert.DeserializeObject<CartDto>
                    (Convert.ToString(response.Result));

                return cartDto;
            }

            return new CartDto();
        }
    }
}
