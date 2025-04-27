using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepsository basketRepsository
        , ICacheRepository cacheRepository, UserManager<AppUser> userManger) : IServiceManger
    {
        public IProductService ProductService { get; }= new ProductService(unitOfWork, mapper);
       //IBasketService BasketService { get; } = new BasketService(basketRepsository , mapper);
        public IBasketService BasketService { get;  } = new BasketService(basketRepsository, mapper);
        public ICacheService CacheService { get; } = new CacheService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManger);
    }
}
