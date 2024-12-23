using eCommerceApp.Application.DTOs.Category;
using Microsoft.AspNetCore.Http;

namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProductDto : ProductBase
    {
        public Guid Id { get; set; }
        public GetCategoryDto? Category { get; set; }
        public string? Image { get; set; }


    }
}
