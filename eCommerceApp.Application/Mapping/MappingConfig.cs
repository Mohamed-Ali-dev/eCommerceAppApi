using AutoMapper;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Models;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<Product, GetProductDto>();
        }
    }
}
