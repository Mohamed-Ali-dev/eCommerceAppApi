using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
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
        public async Task<IActionResult> Add(CreateCategoryDto categoryDto)
        {
            var result = await categoryService.AddAsync(categoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            var result = await categoryService.UpdateAsync(categoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
