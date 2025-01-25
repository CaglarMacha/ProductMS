using Microsoft.EntityFrameworkCore;
using PMS.EntityFrameworkCore.Core;
using PSM.Domain.Products;
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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            return await dbContext.Products.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Product>> GetListAsync()
        {
            return await dbContext.Products
                .Include(p => p.Category) 
                .ToListAsync();
        }

        public Task<List<Product>> GetListAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetProductByTitleAsync(string title)
        {
            return await dbContext.Products.Where(p => p.Title == title && !p.IsDeleted).FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductWithCategoryAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> InsertAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
