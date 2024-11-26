﻿using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Models;
using System.Linq.Expressions;

namespace eCommerceApp.Application.Services.Implementation
{
    internal class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategoryDto categoryDto)
        {
            var mappedData = mapper.Map<Category>(categoryDto);
            int result = await categoryInterface.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category added!")
             : new ServiceResponse(false, "Category failed to be added!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category deleted!")
                : new ServiceResponse(false, "Category not found or failed delete!");
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var rowData = await categoryInterface.GetAllAsync();
            if (!rowData.Any())
                return [];
            return mapper.Map<IEnumerable<GetCategoryDto>>(rowData);
        }

        public async Task<GetCategoryDto> GetAsync(Expression<Func<Category, bool>> filter)
        {
            var rowData = await categoryInterface.GetAsync(filter);
            if (rowData == null)
                return new GetCategoryDto();
            return mapper.Map<GetCategoryDto>(rowData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategoryDto categoryDto)
        {
            var mappedData = mapper.Map<Category>(categoryDto);
            int result = await categoryInterface.UpdateAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category updated!")
             : new ServiceResponse(false, "Category failed to be updated!");
        }
    }
}
