using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Aliases must come before any usage
using AssemblyService = Services.AssemblyReference;

using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Store.G03.API.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Store.G03.API.Extensions;


namespace Store.G03.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ////Add service container
            //builder.Services.RegisterAllServices(builder.Configuration);

            //builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //builder.Services.AddInfrastructureServices(builder.Configuration);
            //builder.Services.AddApplicationServices();


            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IServiceManger, ServiceManger>();
            //builder.Services.AddAutoMapper(typeof(AssemblyService).Assembly);



            //builder.Services.Configure<ApiBehaviorOptions>(
            //    config =>
            //    {
            //        config.InvalidModelStateResponseFactory = (actionContext) =>
            //        {
            //            var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any()).Select(
            //                  m => new ValidationError()
            //                  {
            //                      Field = m.Key,
            //                      Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
            //                  }

            //                  );

            //            var response = new ValidationErrorResponse()
            //            {
            //                Errors = errors

            //            };
            //            return new BadRequestObjectResult("");
            //        };
            //    }

            //    );
            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();
          await  app.ConfigureMiddlewares();
  
            app.Run();
        }
    }
}
