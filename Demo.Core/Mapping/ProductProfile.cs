using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapping
{
    public class ProductProfile:Profile
    {
        public ProductProfile() {
            CreateMap<Product,ProductDTO>()
                .ForMember(p=>p.BrandName,option=>option.MapFrom (p=>p.Brand.Name))
                .ForMember(p => p.TypeName, option => option.MapFrom(p => p.Type.Name));
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductType, TypeBrandDTO>();
            CreateMap<TypeBrandDTO, ProductType>();
            CreateMap<TypeBrandDTO, ProductBrand>();
            CreateMap<ProductBrand, TypeBrandDTO>();
            
        } 
    }
}
