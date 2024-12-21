﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class GetArchiveDto
    {
        public string? ProductName { get; set; }
        public int QuantityOrdered { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal AmountPayed { get; set; }
        public DateTime DataPurchased { get; set; }
    }
}
