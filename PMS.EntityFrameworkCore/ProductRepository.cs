using Microsoft.EntityFrameworkCore;
using PMS.EntityFrameworkCore.Core;
using PSM.Domain.Entities;
using PSM.Domain.Products;
using PSM.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMS.EntityFrameworkCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly PMSDbContext dbContext;
        public ProductRepository(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var data = await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await dbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Product?> GetAsync(Guid id)
        {
            return await dbContext.Products.Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync();
        }
        public async Task<Product?> GetActiveProductAsync(Guid id)
        {
            return await dbContext.Products.Where(x => x.Id == id && !x.IsDeleted && x.IsActive).SingleOrDefaultAsync();
        }
        public async Task<List<Product>> GetListAsync()
        {
            return await dbContext.Products
                .Include(p => p.Category) 
                .ToListAsync();
        }

        public async Task<List<Product>> GetListAsync(Expression<Func<Product, bool>> predicate)
        {
            var query = dbContext.Products.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<Product?> GetProductByTitleAsync(string title)
        {
            return await dbContext.Products.Where(p => p.Title == title && !p.IsDeleted).FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<Product>> GetFilteredProductsAsync(string? keyword = null, int? minStockQuantity = null, int? maxStockQuantity = null)
        {
            IQueryable<Product> query = dbContext.Products.Include(p => p.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => (p.Title.Contains(keyword) ||
                                         p.Description.Contains(keyword) ||
                                         p.Category.Name.Contains(keyword) || p.NormalizedTitle.Contains(keyword.ToLowerInvariant().Normalize())));
            }

            if (minStockQuantity.HasValue)
            {
                query = query.Where(p => p.StockQuantity >= minStockQuantity.Value);
            }

            if (maxStockQuantity.HasValue)
            {
                query = query.Where(p => p.StockQuantity <= maxStockQuantity.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetFilteredProductsAsync(Expression<Func<Product, bool>> predicate)
        {
            return await dbContext.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(predicate)
                .ToListAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var product = await dbContext.Products.Where(x => x.Id == id).SingleOrDefaultAsync();
            product.IsDeleted = true;
            product.IsActive = false;
            await UpdateAsync(product);
        }
        public async Task<Product> DeleteAsync(Product entity)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            return await UpdateAsync(entity);
        }
    }
}
