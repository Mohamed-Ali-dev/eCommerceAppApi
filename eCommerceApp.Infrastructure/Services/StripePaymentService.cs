using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount,
            IEnumerable<Product> cartProducts, IEnumerable<ProcessCartDto> carts, string? domain)
        {
            try
            {

            
               var lineItems = new List<SessionLineItemOptions>();
              
               foreach (var product in cartProducts)
               {
                   var cart = carts.FirstOrDefault(c => c.ProductId == product.Id);
              
                   lineItems.Add(new SessionLineItemOptions
                   {
                       PriceData = new SessionLineItemPriceDataOptions
                       {
                           Currency = "usd",
                           ProductData = new SessionLineItemPriceDataProductDataOptions
                           {
                               Name = product.Name,
                               Description = product.Description
                           },
                           UnitAmount = (long)(product.Price * 100),
                       },
                       Quantity = cart.Quantity,
                   });
               }
                //var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                var options = new SessionCreateOptions
               {
                   PaymentMethodTypes = ["card"],
                   LineItems = lineItems,
                   Mode = "payment",
                   SuccessUrl = domain + $"payment-success",
                   CancelUrl = domain + $"payment-cancel"
               };
               var service = new SessionService();
               Session session = await service.CreateAsync(options);
               return new ServiceResponse(true, session.Url);

            }
            catch(Exception ex) 
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
