using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
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

        public DbInitializer(StoreDbContext context)
        {
            _context=context;
           

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
            }
            catch { }
        }
    }
}
