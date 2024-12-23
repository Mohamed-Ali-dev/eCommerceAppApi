using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Models;
using System.Linq.Expressions;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<GetCategoryDto> GetAsync(Expression<Func<Category, bool>> filter, bool tracking = false);
        Task<ServiceResponse> AddAsync(CreateCategoryDto category);
        Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<IEnumerable<GetProductDto>> GetProductsByCategory(Guid categoryId);
    }
}
