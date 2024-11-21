using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategoryDto : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProductDto>? Products { get; set; }

    }
}
