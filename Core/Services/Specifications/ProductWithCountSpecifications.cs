using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
   public class ProductWithCountSpecifications: BaseSpecifications<Products,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParameters productSpecParams) : base(P =>
         (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search.ToLower()))&&
        (!productSpecParams.BrandId.HasValue || P.BrandId == productSpecParams.BrandId) &&
        (!productSpecParams.TypeId.HasValue || P.TypeId == productSpecParams.TypeId))
        {
            
        }
    }
}
