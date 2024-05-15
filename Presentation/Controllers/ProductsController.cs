using DTOs.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductServices productServices { get; set; }
        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        // ___________________________ 1- Create ___________________________
        [HttpPost]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Create(CreateOrUpdateProductDTO productDTO)
        {
            var idClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
            if (idClaims == null)
            {
                return Unauthorized("Role claim not found in user claims !....");
            }
            else
            {
                var vendorID = idClaims.Value;
                var createProduct = await productServices.Create(productDTO, vendorID);
                if (createProduct == null)
                    return BadRequest("The 'Category ID' was not found in the table of categories, please try again....");
                else
                    return Ok(createProduct);
            }
        }

        //_____________________________ 2- Read _____________________________
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string? categoryName)
        {
            if (categoryName != null)
            {
                var productsByCatecory = await productServices.GetbyCategory(categoryName);
                if (productsByCatecory != null)
                    return Ok(productsByCatecory);
                else
                    return BadRequest($"The Category '{categoryName}' wasn't found!.....");
            }
            else
            {
                var products = await productServices.GetAll();
                if (products != null)
                    return Ok(products);
                else
                    return BadRequest("There is a problem in getting prodactes data !.....");
            }
        }
        [HttpGet("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            if (ID > 0)
            {
                var product = await productServices.GetOne(ID);
                if (product != null)
                    return Ok(product);
                else
                    return BadRequest("There is no product with this ID !....");
            }
            else
                return BadRequest("The ID must be greater then 0 !....");
        }

        // ____________________________ 3- Update ____________________________
        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> Update(CreateOrUpdateProductDTO productDTO, int ID)
        {
            string vendorID;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
                return Unauthorized("Role claim not found in user claims !....");
            else
            {
                var role = roleClaims.Value;
                switch (role)
                {
                    case "Vendor":
                        var vendorIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
                        if (vendorIDClaims == null)
                            return Unauthorized("User ID claim not found in user claims !....");
                        vendorID = vendorIDClaims.Value;
                        break;
                    case "Admin":
                        vendorID = null;
                        break;
                    default:
                        return Forbid("The user unauthorized to use this action !....");
                }
                var updatedProduct = await productServices.Update(productDTO, ID, vendorID);
                if (updatedProduct != null)
                    return Ok(updatedProduct);
                else
                    return BadRequest("The 'product ID' or the 'Category ID' is not found for this user !...");
            }
        }

        // ____________________________ 4- Delete ____________________________
        [HttpDelete("{ID:int}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> Delete(int ID)
        {
            string vendorID;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
                return Unauthorized("Role claim not found in user claims !....");
            else
            {
                var role = roleClaims.Value;
                switch (role)
                {
                    case "Vendor":
                        var vendorIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
                        if (vendorIDClaims == null)
                            return Unauthorized("User ID claim not found in user claims !....");
                        vendorID = vendorIDClaims.Value;
                        break;
                    case "Admin":
                        vendorID = null;
                        break;
                    default:
                        return Forbid("The user unauthorized to use this action !....");
                }
                var deleteProduct = await productServices.Delete(ID, vendorID);
                if (!deleteProduct)
                    return BadRequest("There is no product with this ID for this user !....");
                else
                    return Ok($"The Product '{ID}' was deleted successfully !....");
            }
        }
    }
}
