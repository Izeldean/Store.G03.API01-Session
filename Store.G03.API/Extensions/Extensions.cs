using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Identity;
using Services;
using Shared;
using Shared.ErrorModels;
using Microsoft.IdentityModel.Tokens;
using Store.G03.API.MiddleWare;
using System.Runtime.CompilerServices;
using System.Text;

namespace Store.G03.API.Extensions
{
    public static class Extensions
    {
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection ConfigureJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                    };


                });
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
            app.UseAuthentication();    
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
            services.ConfigureService();
            services.AddInfrastructureServices(configuration);
            services.AddIdentityServices();
            services.AddApplicationServices(configuration);

            services.ConfigureJwtServices(configuration);

            return services;
        }
    }


}
