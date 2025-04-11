using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
 
    

        public Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            //get all products through productRepository 
           var products= unitOfWork.GetRepository<Products, int>().GetAllAsync();
            // mapping IEnumabler<Product> to <IEnumerable<ProductResultDto>:AutoMapper
            var result =mapper.Map<IEnumerable<ProductResultDto>>(products);
            return (Task<IEnumerable<ProductResultDto>>)result;
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
         var product= await unitOfWork.GetRepository<Products, int >().GetAsync(id);
            if (product is null) { return null; }
          var result=  mapper.Map<ProductResultDto>(product);
            return result;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync(int id)
        {
           var brands= await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
           var result= mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

   

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync(int id)
        {
            var types= await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

      
    }
}
