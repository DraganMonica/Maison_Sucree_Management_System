using Maison_Sucree.Web.Models;
using Maison_Sucree.Web.Service.IService;
using Maison_Sucree.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Maison_Sucree.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Dashboard()
        {
            var response = await _orderService.GetAllOrders("");
            var orders = new List<OrderHeaderDto>();

            if (response != null && response.IsSuccess)
                orders = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));

            var vm = new AdminDashboardViewModel
            {
                TotalOrders         = orders.Count,
                TotalRevenue        = orders.Where(o => o.Status == SD.Status_Approved || o.Status == SD.Status_Completed).Sum(o => o.OrderTotal),
                TotalCustomers      = orders.Select(o => o.UserId).Distinct().Count(),
                ActiveOrders        = orders.Count(o => o.Status == SD.Status_Approved || o.Status == SD.Status_ReadyForPickup),
                PendingCount        = orders.Count(o => o.Status == SD.Status_Pending),
                ApprovedCount       = orders.Count(o => o.Status == SD.Status_Approved),
                ReadyForPickupCount = orders.Count(o => o.Status == SD.Status_ReadyForPickup),
                CompletedCount      = orders.Count(o => o.Status == SD.Status_Completed),
                CancelledCount      = orders.Count(o => o.Status == SD.Status_Cancelled),
                RecentOrders        = orders.OrderByDescending(o => o.OrderHeaderId).Take(5).ToList()
            };

            return View(vm);
        }


        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if (!User.IsInRole(SD.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }

            ResponseDto response = _orderService.GetAllOrders(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                //am deserializat to a list of order header dto
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
                switch (status)
                {
                    case "approved":
                        list = list.Where(u => u.Status == SD.Status_Approved);
                        break;
                    case "readyforpickup":
                        list = list.Where(u => u.Status == SD.Status_ReadyForPickup);
                        break;
                    case "cancelled":
                        list = list.Where(u => u.Status == SD.Status_Cancelled);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list });
        }

        
        [HttpGet]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }

            if(!User.IsInRole(SD.RoleAdmin)&& userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }

            return View(orderHeaderDto);
        }



        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId) 
        { 
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
            }
            else
            {
                TempData["error"] = "Error updating status";
            }
            return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });

        }
         

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
            }
            else
            {
                TempData["error"] = "Error updating status";
            }
            return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
        }


        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order cancelled successfully";
            }
            else
            {
                TempData["error"] = "Order cancelled but refund failed";
            }
            return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
        }
        
    }
}
