using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Domain.Models.Cart;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class CartRepository(AppDbContext context) : ICart
    {
        public async Task<int> SaveCheckoutHistory(IEnumerable<Archive> checkouts)
        {
           context.CheckoutArchives.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}
