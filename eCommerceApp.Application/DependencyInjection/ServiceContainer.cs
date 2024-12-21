using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementation;
using eCommerceApp.Application.Services.Implementation.Authentication;
using eCommerceApp.Application.Services.Implementation.Cart;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Application.Validations;
using eCommerceApp.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();

            services.AddFluentValidationAutoValidation();
            //add all the validation classes in the assembly
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            services.AddScoped<IValidationService, ValidationService>();
           services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
