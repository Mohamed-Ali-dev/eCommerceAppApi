using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        //called by ui
        public async Task<IActionResult> Checkout(CheckoutDto checkout)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await cartService.Checkout(checkout);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("save-checkout")]
        

        public async Task<IActionResult> SaveCheckout(IEnumerable<CreateArchiveDto> archives)
        {
            var result =await cartService.SaveCheckoutHistory(archives);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-archives")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCheckoutHistory()
        {
            var archives = await cartService.GetArchives();
            return archives.Any() ? Ok(archives) : NotFound();
        }
    }
}
