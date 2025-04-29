using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;

        public DbInitializer(StoreDbContext context, StoreIdentityDbContext identityDbContext,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            _context=context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _roleManger = roleManger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Create Database If it does not exist && Apply To Any Pending Migration
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                //Data seeding
                //Seeding ProductType from JSon
                if (!_context.ProductTypes.Any())
                {
                    //1- Read AllData From Types Json FIle as string
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // 2- Transform string to C# objects [List<ProductTypes>] from Json files as string
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    // 3- Add List<ProductType> to database from Json files as string
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();

                    }

                }

                // Seeding ProductBRands from JSon

                //Data seeding
                //Seeding ProductType from JSon
                if (!_context.ProductBrands.Any())
                {
                    //1- Read AllData From Types Json FIle as string
                    var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // 2- Transform string to C# objects [List<ProductTypes>] from Json files as string
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    // 3- Add List<ProductType> to database from Json files as string
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();

                    }

                }

                // Seeding Products from JSon

                if (!_context.Products.Any())
                {
                    //1- Read AllData From Types Json FIle as string
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // 2- Transform string to C# objects [List<ProductTypes>] from Json files as string
                    var product = JsonSerializer.Deserialize<List<Products>>(productsData);
                    // 3- Add List<ProductType> to database from Json files as string
                    if (product is not null && product.Any())
                    {
                        await _context.Products.AddRangeAsync(product);
                        await _context.SaveChangesAsync();

                    }

                }

                if (!_context.DeliveryMethods.Any())
                {
                    //1- Read AllData From Types Json FIle as string
                    var Deliverydata = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\delivery.json");

                    // 2- Transform string to C# objects [List<DeliveryMethod>] from Json files as string
                    var deliveryM = JsonSerializer.Deserialize<List<DeliveryMethod>>(Deliverydata);
                    // 3- Add List<ProductType> to database from Json files as string
                    if (deliveryM is not null && deliveryM.Any())
                    {
                        await _context.DeliveryMethods.AddRangeAsync(deliveryM);
                        await _context.SaveChangesAsync();

                    }

                }

            }
            catch { }
        }

        public async Task InitializeIdentityAsync()
        {
            //Create Database if it doesnot exists && Apply to Any Pending Migration
            if (_identityDbContext.Database.GetPendingMigrations().Any()) { 
             await _identityDbContext.Database.MigrateAsync();
            }


            if (!_roleManger.Roles.Any()) {
                await _roleManger.CreateAsync(new IdentityRole()
                {
                    Name="Admin"

                });

                await _roleManger.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"

                });
            }

            //Seeding
            if (!_userManager.Users.Any()) {
                var superAdminUser = new AppUser() { 
                
                DisplayName="Super Admin",
                Email ="SuperAdmin@gmail.com",
                UserName="SuperAdmin",
                PhoneNumber="0123456789"
                };

                var AdminUser = new AppUser()
                {

                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };
               await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(AdminUser, "P@ssW0rd");
              await  _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");

            }

        }
    }
}
