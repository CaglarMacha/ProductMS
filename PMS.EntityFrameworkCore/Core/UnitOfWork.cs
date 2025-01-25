using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PSM.Domain;
using PSM.Domain.Categories;
using PSM.Domain.Products;
using PSM.Domain.Shared;

namespace PMS.EntityFrameworkCore.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PMSDbContext dbContext;
        private IDbContextTransaction transaction;
        public UnitOfWork(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            transaction ??= dbContext.Database.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            if (transaction != null)
            {
                await transaction.CommitAsync();
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
