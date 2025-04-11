using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Products, ProductResultDto>().ForMember(P=> P.BrandName, o => o.MapFrom(s =>s.ProductBrand.Name));
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
