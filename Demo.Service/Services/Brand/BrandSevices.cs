using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Brand
{
    public class BrandSevices : IBrandService
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandSevices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TypeBrandDTO>> GetAllBrandsAsync()
        {
            var brandspec = new BaseSpecification<ProductBrand, int>();
            var Brands = await _unitOfWork.CreateRepository<ProductBrand, int>().GetAllAsync(brandspec);
            return _mapper.Map<IEnumerable<TypeBrandDTO>>(Brands);

        }


    }
}
