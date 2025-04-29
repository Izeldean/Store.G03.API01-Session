﻿using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<Products> Products{ get; set; }
        public DbSet<ProductBrand> ProductBrands{ get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

            base.OnModelCreating(modelBuilder);
          
        }
    }
}
