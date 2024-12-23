using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

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
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add([FromForm] CreateProductDto productDto)
        {
            //check on the categoryId
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.AddAsync(productDto);
            return result.Success? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromForm] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.UpdateAsync(productDto);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
