using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.Models;
using Demo.Core.productParams;
using Demo.Core.ProductResponse;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Core.Specifications;
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

       

        public async Task<ProductRespons<ProductDTO>> GetAllProductAsync(ProductParams productParams)
        {
            var productspec = new ProductSpecification(productParams);


            var ProductRepo = _unitOfWork.CreateRepository<Product, int>();

            var Product = await ProductRepo.GetAllWihSpecAsync(productspec);

            var ProductDTO = _mapper.Map<IEnumerable<ProductDTO>>(Product);

            var countspec = new ProductsCount(productParams);
            var count =await _unitOfWork.CreateRepository<Product, int>().CountAsync(countspec);
            

            return new ProductRespons<ProductDTO>(productParams.pageSize.Value, productParams.pageIndex.Value, count, ProductDTO);
        }

    

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var productspec = new ProductSpecification(id);

            var product = await _unitOfWork.CreateRepository<Product, int>().GetByIdWihSpecAsync(productspec);

            var ProductDTO = _mapper.Map<ProductDTO>(product);

            return ProductDTO;


        }


     
    }
}
