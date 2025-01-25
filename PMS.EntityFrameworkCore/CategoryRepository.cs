using Microsoft.EntityFrameworkCore;
using PMS.EntityFrameworkCore.Core;
using PSM.Domain.Categories;
using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMS.EntityFrameworkCore
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PMSDbContext dbContext;
        public CategoryRepository(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            var data = await dbContext.Categorys.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await dbContext.Categorys.Where(p => p.Name == name).SingleOrDefaultAsync();
        }

        public Task<List<Category>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetListAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Category> InsertAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
