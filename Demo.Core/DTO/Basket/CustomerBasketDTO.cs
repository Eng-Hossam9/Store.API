using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTO.Basket
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

    }
}
