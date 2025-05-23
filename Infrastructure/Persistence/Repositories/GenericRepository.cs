﻿using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }


       public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
        {
            if (typeof(TEntity) == typeof(Products)) {
                if (trackChanges) { return await _context.Products.Include(P=> P.ProductBrand).Include(P => P.ProductType).
                        ToListAsync() as IEnumerable<TEntity>; }
                return await _context.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).AsNoTracking().
                        ToListAsync() as IEnumerable<TEntity>;;

            }
            if (trackChanges) { return await _context.Set<TEntity>().AsTracking().ToListAsync(); }
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }


        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Products)) { 

                return await _context.Products.Include(P=> P.ProductType).Include(P=> P.ProductType).FirstOrDefaultAsync(
                    P=> P.Id == id as int?) as TEntity;
            }
                return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
          _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        Task<IEnumerable<TEntity?>> IGenericRepository<TEntity, TKey>.GetAsync(TKey id)
        {
            throw new NotImplementedException();
        }
    }


}
