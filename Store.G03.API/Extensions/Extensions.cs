using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Identity;
using Services;
using Shared.ErrorModels;
using Store.G03.API.MiddleWare;
using System.Runtime.CompilerServices;

namespace Store.G03.API.Extensions
{
    public static class Extensions
    {
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>(); 
            return services;
        }




        private static IServiceCollection AddSwaggerInServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;

            }


        private static IServiceCollection ConfigureService(this IServiceCollection services) {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>

                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any()).Select(
                        m => new ValidationError()
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                        }
                        );
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult("");
                };
            });
            return services;
        }


        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app) {
            await  app.InitializeDatabaseAsync();

            app.GlobalErrorHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //--> used for media and pictures
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }

      private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app) {

            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            #endregion
            return app;
        }

        private static WebApplication GlobalErrorHandling(this WebApplication app) { 
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        
        }

        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddBuiltInServices();
            services.AddSwaggerInServices();
           
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();
            services.AddIdentityServices();
            services.ConfigureService();


            return services;
        }
    }


}
