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
            CreateMap<Products, ProductResultDto>()
                .ForMember(P => P.BrandName, o => o.MapFrom(s => s.ProductBrand.Name)).
                ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name))
                //.ForMember(d => d.PictureUrl , o => o.MapFrom(s=>$"https://localhost:7046/{s.PictureUrl}"))
               .ForMember(d => d.PictureUrl , o=> o.MapFrom<PictureUrlReslover>())
                ;
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
