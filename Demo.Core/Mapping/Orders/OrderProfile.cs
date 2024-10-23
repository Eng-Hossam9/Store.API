using AutoMapper;
using Demo.Core.DTO.Orders;
using Demo.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapping.Orders
{
    public class OrderProfile:Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderResponseDTO>()
                     .ForMember(d=>d.DeliveryMethod,option=>option.MapFrom(s=>s.DeliveryMethod.ShortName)) 
                     .ForMember(d=>d.DeliveryMethodCost,option=>option.MapFrom(s=>s.DeliveryMethod.Cost));

            CreateMap<Address, AddressDTO>().ReverseMap();
             
            CreateMap<OrderItem,OrderItemDTO>()
                     .ForMember(d=>d.ProductId,option=>option.MapFrom(s=>s.Product.Id))
                     .ForMember(d=>d.ProductName,option=>option.MapFrom(s=>s.Product.ProductName))
                     .ForMember(d => d.PictureUrl, option => option.MapFrom(s => $"{configuration["BaseUrl"]}{s.Product.PictureUrl}"))
                ;



        }
    }
}
