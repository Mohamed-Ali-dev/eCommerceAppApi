using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync();
        Task<GetProductDto> GetAsync(Expression<Func<Product, bool>> filter, bool tracking = false);
        Task<ServiceResponse> AddAsync(CreateProductDto product);
        Task<ServiceResponse> UpdateAsync(UpdateProductDto product);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
