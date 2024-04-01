using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Shared.Dto;
using Shared.Models;


namespace StoreAPI.Controllers
{
    //[Authorize (Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


    
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                List<Category> categories = await _categoryService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest("an error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                Category? category = await _categoryService.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest("cannt find category");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto category)
        {
            try
            {
                Category newCategory = await _categoryService.CreateCategory(category);
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest("cannot create category");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> PutCategory(int id, CategoryDto category)
        {
            if (id != category.Id)
            {
                return BadRequest("id does not match");
            }
            try
            {
                Category? newCategory = await _categoryService.UpdateCategory(id, category);
                if (newCategory == null)
                {
                    return NotFound();
                }

                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                Category? category = await _categoryService.DeleteCategory(id);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest("cannot delete category");
            }
        }

    }
}