using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound();
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await productService.GetAsync(u => u.Id == id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProductDto productDto)
        {
            var result = await productService.AddAsync(productDto);
            return result.Success? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductDto productDto)
        {
            var result = await productService.UpdateAsync(productDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
