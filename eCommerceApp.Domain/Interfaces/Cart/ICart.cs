using eCommerceApp.Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces.Cart
{
    public interface ICart
    {
        Task<int> SaveCheckoutHistory(IEnumerable<Archive> checkouts);
    }
}
