using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class CheckoutDto
    {
        [Required]
        public Guid PaymentMethodId { get; set; }
        [Required]
        public IEnumerable<ProcessCartDto> Carts { get; set; }
    }
}
