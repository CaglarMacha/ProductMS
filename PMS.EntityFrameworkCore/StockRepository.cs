using PMS.EntityFrameworkCore.Core;
using PSM.Domain.Products;
using PSM.Domain.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMS.EntityFrameworkCore
{
    public class StockRepository : IStockRepository
    {
        private readonly PMSDbContext dbContext;
        public StockRepository(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Stock> CreateAsync(Stock entity)
        {
            var data = await dbContext.Stocks.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stock>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Stock>> GetListAsync(Expression<Func<Stock, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> InsertAsync(Stock entity)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> UpdateAsync(Stock entity)
        {
            throw new NotImplementedException();
        }
    }
}
