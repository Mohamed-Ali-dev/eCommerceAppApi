using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result = await authenticationService.CreateUser(user);
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LogInUser user)
        {
            var inputRefreshToken = Request.Cookies["refreshToken"];
            var result = await authenticationService.LoginUser(user, inputRefreshToken);
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);


            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult> ReviveToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await authenticationService.ReviveToken(refreshToken);
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

    }
}
