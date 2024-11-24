using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category
{
    public class UpdateCategoryDto : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
