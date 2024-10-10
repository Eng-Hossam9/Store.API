using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapping
{
    public class ProductProfile:Profile
    {
        public ProductProfile(IConfiguration configuration) {
            CreateMap<Product,ProductDTO>()
                .ForMember(p=>p.BrandName,option=>option.MapFrom (p=>p.Brand.Name))
                .ForMember(p => p.TypeName, option => option.MapFrom(p => p.Type.Name))
                //  .ForMember(p=>p.PictureUrl,option=>option.MapFrom(p=> $"{configuration["BaseUrl"]}{p.PictureUrl}"));
                .ForMember(p => p.PictureUrl, option => option.MapFrom(new PictureURlResolver(configuration)));
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductType, TypeBrandDTO>();
            CreateMap<TypeBrandDTO, ProductType>();
            CreateMap<TypeBrandDTO, ProductBrand>();
            CreateMap<ProductBrand, TypeBrandDTO>();
            
        } 
    }
}
