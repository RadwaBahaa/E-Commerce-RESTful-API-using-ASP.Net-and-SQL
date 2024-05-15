using DTOs.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Interface;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderServices orderServices { get; set; }
        public OrdersController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        // ___________________________ 1- Create ___________________________
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateOrderDTO orderDTO)
        {
            var customerIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
            if (customerIDClaims == null)
                return Unauthorized("User ID claim not found in user claims !....");
            var customerID = customerIDClaims.Value;
            var order = await orderServices.Create(orderDTO, customerID);
            if (order == null)
                return BadRequest("There is a product or more cannot be found, Try again !....");
            else
                return Ok(order);
        }

        // ___________________________ 2- Read ___________________________
        [HttpGet]
        [Authorize(Roles = "User,Admin,Vendor")]
        public async Task<IActionResult> GetAll()
        {
            List<ReadAllOrdersDTO> orders;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
                return Unauthorized("Role claim not found in user claims !....");
            string userRole = roleClaims.Value;
            var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
            if (userIDClaims == null)
                return Unauthorized("User ID claim not found in user claims !....");
            string userID = userIDClaims.Value;

            orders = await orderServices.GetAll(userRole, userID);

            if (orders != null)
                return Ok(orders);
            else
                return BadRequest("There is a problem in getting orders data !.....");
        }

        [HttpGet("{ID:int}")]
        [Authorize(Roles = "User,Admin,Vendor")]
        public async Task<IActionResult> GetOne(int ID)
        {
            ReadAllOrdersDTO order;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
                return Unauthorized("Role claim not found in user claims !....");
            string userRole = roleClaims.Value;
            var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
            if (userIDClaims == null)
                return Unauthorized("User ID claim not found in user claims !....");
            string userID = userIDClaims.Value;

            order = await orderServices.GetOne(ID, userRole, userID);

            if (order != null)
                return Ok(order);
            else
                return BadRequest("There is no order with this ID !....");
        }

        // ___________________________ 3- Update ___________________________
        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateOrderDTO orderDTO, int ID)
        {
            var updateOrder = await orderServices.Update(orderDTO, ID);
            if (updateOrder == null)
                return BadRequest("There is no order with this ID !....");
            else
                return Ok(updateOrder);
        }

        // ____________________________ 4- Delete ____________________________
        [HttpDelete("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ID)
        {
            bool deleteOrder = await orderServices.Delete(ID, null);
            if (!deleteOrder)
                return BadRequest("There is no order with this ID !....");
            else
                return Ok($"The order '{ID}' was deleted successfully !....");
        }
    }
}
