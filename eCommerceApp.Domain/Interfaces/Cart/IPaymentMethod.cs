using eCommerceApp.Domain.Models.Cart;

namespace eCommerceApp.Domain.Interfaces.Cart
{
    public interface IPaymentMethod
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
    }
}
