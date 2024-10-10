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

namespace Demo.Service.Services.Type
{
    public class TypeServices : ITypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TypeBrandDTO>> GetAllTypesAsync()
        {
            var typespec = new BaseSpecification<ProductType, int>();
            var TypeRepo = _unitOfWork.CreateRepository<ProductType, int>();
            var types = await TypeRepo.GetAllAsync(typespec);
            var typesDTO = _mapper.Map<IEnumerable<TypeBrandDTO>>(types);
            return typesDTO;
        }
    }
}
