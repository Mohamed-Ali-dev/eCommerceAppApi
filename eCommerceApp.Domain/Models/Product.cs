using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public decimal  Price { get; set; }
        public string? Image { get; set; }
        public int  Quantity { get; set; }
        public Category? Categoory { get; set; }
        public Guid CategoryId { get; set; }

    }

}
