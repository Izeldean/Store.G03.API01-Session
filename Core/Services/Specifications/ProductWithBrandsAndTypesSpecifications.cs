using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications:BaseSpecifications<Products, int>
    {
       

        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id==id)
        {
            ApplyIncludes();
        }

        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParameters  productSpecParams ) : base(P=> 
        (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search.ToLower()))&& 
        (!productSpecParams.BrandId.HasValue || P.BrandId == productSpecParams.BrandId) &&
        (!productSpecParams.TypeId.HasValue || P.TypeId == productSpecParams.TypeId))
        {
            ApplyIncludes();
         ApplySorting(productSpecParams.Sort);
            ApplyPagination(productSpecParams.PageIndex, productSpecParams.PageSize);

        }
        public void ApplyIncludes() {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
          
        }
        private void ApplySorting(string? sort) {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }

            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }

    }
}
