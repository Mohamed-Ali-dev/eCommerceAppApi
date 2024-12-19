using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementation.Cart
{
    internal class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethodDto>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentMethods();
            if (!methods.Any())
                return [];

            return mapper.Map<IEnumerable<GetPaymentMethodDto>>(methods);
        }
    }
}
