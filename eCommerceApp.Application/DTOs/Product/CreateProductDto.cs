using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class CreateProductDto : ProductBase
    {
        [Required]
        public IFormFile? Image { get; set; }

    }
}
