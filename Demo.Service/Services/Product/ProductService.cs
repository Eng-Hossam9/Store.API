using AutoMapper;
using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.Models;
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

     

       


       

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var productspec = new ProductSpecification();


            var ProductRepo = _unitOfWork.CreateRepository<Product, int>();

            var Product = await ProductRepo.GetAllAsync(productspec);

            var ProductDTO = _mapper.Map<IEnumerable<ProductDTO>>(Product);

            return ProductDTO;
        }

    

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var productspec = new ProductSpecification(id);

            var product = await _unitOfWork.CreateRepository<Product, int>().GetByIdAsync(productspec);

            var ProductDTO = _mapper.Map<ProductDTO>(product);

            return ProductDTO;


        }


     
    }
}
