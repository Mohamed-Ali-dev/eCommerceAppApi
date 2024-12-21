using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            // fix that the product array returned empty
            var data = await categoryService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound();
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await categoryService.GetAsync(u => u.Id == id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add(CreateCategoryDto categoryDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            var result = await categoryService.AddAsync(categoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(categoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("products-by-category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory(Guid categoryId)
        {
            var results = await categoryService.GetProductsByCategory(categoryId);
            return results.Any() ? Ok(results) : NotFound();
        }
    }
}
