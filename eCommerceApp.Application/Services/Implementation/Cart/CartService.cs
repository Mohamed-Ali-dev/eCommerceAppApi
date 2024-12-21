using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Domain.Models;
using eCommerceApp.Domain.Models.Cart;

namespace eCommerceApp.Application.Services.Implementation.Cart
{
    public class CartService(ICart cartInterface, IMapper mapper,IPaymentService paymentService,
        IGeneric<Product> productInterface, IPaymentMethodService paymentMethodService, IUserManagement userManagement) : ICartService
    {
        public async Task<ServiceResponse> Checkout(CheckoutDto checkout)
        {
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods =  await paymentMethodService.GetPaymentMethods();
            if(checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
            {
                return await paymentService.Pay(totalAmount, products, checkout.Carts, null);
            }
            return new ServiceResponse(false, "Invalid payment method");

        }

        public async Task<IEnumerable<GetArchiveDto>> GetArchives()
        {
            var history = await cartInterface.GetAllCheckoutHistory();
            if (history == null) return [];

            var groupByCustomerId = history.GroupBy(x => x.UserId).ToList();
            var products = await productInterface.GetAllAsync();
            var archives = new List<GetArchiveDto>();
            foreach (var archive in groupByCustomerId)
            {
                var user = await userManagement.GetUserById(archive.Key!);
                foreach (var item in archive)
                {
                   var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                    archives.Add(new GetArchiveDto
                    {
                        CustomerName = user.FullName,
                        CustomerEmail = user.Email,
                        ProductName = product!.Name,
                        AmountPayed = item.Quantity * product.Price,
                        QuantityOrdered = item.Quantity,
                        DataPurchased = item.CreatedData
                    });
                }

            }
            return archives;

        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateArchiveDto> createArchives)
        {
            var archives = mapper.Map<IEnumerable<Archive>>(createArchives);
            var result = await cartInterface.SaveCheckoutHistory(archives);
            return result > 0 ? new ServiceResponse(true, "Checkout archived") :
                new ServiceResponse(false, "Error occurred in saving");
        }
        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCartDto> carts)
        {
            if (!carts.Any()) return ([], 0);

            var products = await productInterface.GetAllAsync();
            if (products.Any()) return ([], 0);

            var cartProducts = carts.Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null).ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmount);
        }
    }
}
