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

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();
          await  app.ConfigureMiddlewares();
  
            app.Run();
        }
    }
}
