using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class UpdateProductDto : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
