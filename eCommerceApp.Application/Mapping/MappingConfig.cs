using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Models;
using eCommerceApp.Domain.Models.Cart;
using eCommerceApp.Domain.Models.Identity;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(p =>p.Image, opt => opt.Ignore());

            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateProductDto, Product>()
                .ForMember(p => p.Image, opt => opt.Ignore());


            CreateMap<Category, GetCategoryDto>();
            CreateMap<Product, GetProductDto>()
                .ForMember(p =>p.Image, opt =>opt.Ignore());

            CreateMap<CreateUser, AppUser>();
            CreateMap<LogInUser, AppUser>();

            CreateMap<PaymentMethod, GetPaymentMethodDto>();
            CreateMap<CreateArchiveDto, Archive>();

        }
    }
}
