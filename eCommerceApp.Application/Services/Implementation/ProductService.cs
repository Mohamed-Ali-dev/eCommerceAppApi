using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Models;
using System.Linq.Expressions;

namespace eCommerceApp.Application.Services.Implementation
{
    public class ProductService(IGeneric<Product> productInterface, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProductDto productDto)
        {
            var mappedData = mapper.Map<Product>(productDto);
            int result = await productInterface.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Product added!")
             : new ServiceResponse(true, "Product failed to be added!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await productInterface.DeleteAsync(id);
            
            return result > 0 ? new ServiceResponse(true, "Product deleted!") 
                : new ServiceResponse(true, "Product not found or failed to be delete!");
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var rowData = await productInterface.GetAllAsync();
            if (!rowData.Any()) 
                return [];
            return mapper.Map<IEnumerable<GetProductDto>>(rowData);
        }

        public async Task<GetProductDto> GetAsync(Expression<Func<Product, bool>> filter)
        {
            var rowData = await productInterface.GetAsync(filter);
            if (rowData == null)
                return new GetProductDto();
            return mapper.Map<GetProductDto>(rowData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            var result = await productInterface.UpdateAsync(product);
            return result > 0 ? new ServiceResponse(true, "Product updated!")
             : new ServiceResponse(true, "Product failed to be updated!");
        }
    }
}
