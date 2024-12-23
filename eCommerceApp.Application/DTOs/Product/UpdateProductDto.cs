using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class UpdateProductDto : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public IFormFile? Image { get; set; }

    }
}
