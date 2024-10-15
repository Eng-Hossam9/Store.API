using AutoMapper;
using Demo.Core.DTO.Basket;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapping
{
    public class CustomerBasketProfile:Profile
    {
        public CustomerBasketProfile() 
        {
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
        }
    }
}
