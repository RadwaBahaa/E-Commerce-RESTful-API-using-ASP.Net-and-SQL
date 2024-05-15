using DTOs.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryServices categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        // ___________________________ 1- Create ___________________________
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateOrUpdateCategoryDTO categoryDTO)
        {
            var createCategory = await categoryServices.Create(categoryDTO);
            if (createCategory != null)
                return Ok(createCategory);
            else
                return BadRequest("This Category already exist !...");
        }

        // ___________________________ 2- Read ___________________________
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryServices.GetAll();
            if (categories == null)
                return BadRequest();
            else
                return Ok(categories);
        }

        [HttpGet("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            if (ID > 0)
            {
                var categories = await categoryServices.GetOne(ID);
                if (categories == null)
                    return BadRequest("There is no category with this ID !....");
                else
                    return Ok(categories);
            }
            else
                return BadRequest("The ID must be greater then 0 !....");
        }

        // ___________________________ 3- Update ___________________________
        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CreateOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var getCategore = await categoryServices.GetOne(ID);
            if (getCategore == null)
                return BadRequest("There is no category with this ID !....");
            else
                await categoryServices.Update(categoryDTO, ID);
            return Ok($"The name of category {ID} was updated from '{getCategore.Name}' to be '{categoryDTO.Name}' ....");
        }

        // ___________________________ 4- Delete ___________________________
        [HttpDelete("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteCategory = await categoryServices.Delete(ID);
            if (deleteCategory)
                return Ok($"The category '{ID}' was deleted successfully !....");
            else
                return BadRequest("There is no category with this ID !....");
        }
    }
}
