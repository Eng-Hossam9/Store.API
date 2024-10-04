using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

     

        public async Task<IEnumerable<TypeBrandDTO>> GetAllTypesAsync()
        {
            var TypeRepo = _unitOfWork.CreateRepository<ProductType, int>();
            var types = await TypeRepo.GetAllAsync();
            var typesDTO = _mapper.Map<IEnumerable<TypeBrandDTO>>(types);
            return typesDTO;
        }


        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.CreateRepository<Product, int>().GetByIdAsync(id);

            var ProductDTO = _mapper.Map<ProductDTO>(product);

            return ProductDTO;


        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var ProductRepo = _unitOfWork.CreateRepository<Product, int>();

            var Product = await ProductRepo.GetAllAsync();

            var ProductDTO = _mapper.Map<IEnumerable<ProductDTO>>(Product);

            return ProductDTO;
        }


        public async Task<IEnumerable<TypeBrandDTO>> GetAllBrandsAsync()
        {
            var Brands =await  _unitOfWork.CreateRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeBrandDTO>>(Brands);
            
        }
    }
}
