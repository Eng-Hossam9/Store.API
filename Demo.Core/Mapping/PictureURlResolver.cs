using AutoMapper;
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
    public class PictureURlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public PictureURlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                    return $"{configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty ;   
        }
    }
}
