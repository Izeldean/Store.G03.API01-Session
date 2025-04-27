using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class PictureUrlReslover(IConfiguration configuration) : IValueResolver<Products, ProductResultDto, string>
    {
        public string Resolve(Products source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))return string.Empty;
            return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
