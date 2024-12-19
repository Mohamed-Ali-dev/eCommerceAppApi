using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateArchiveDto> createArchives);
        Task<ServiceResponse> Checkout(CheckoutDto checkout);
    }
}
