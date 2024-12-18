﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Models.Cart
{
    public class PaymentMethod
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  Name { get; set; }
    }
}
