using eCommerceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces.CategorySpecifics
{
    public interface ICategory
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}
