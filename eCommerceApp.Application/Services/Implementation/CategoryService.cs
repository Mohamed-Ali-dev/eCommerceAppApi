using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Models;
using System.Linq.Expressions;

namespace eCommerceApp.Application.Services.Implementation
{
    internal class CategoryService : ICategoryService
    {
        public Task<ServiceResponse> AddAsync(CreateCategoryDto category)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCategoryDto> GetAsync(Expression<Func<Category, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
