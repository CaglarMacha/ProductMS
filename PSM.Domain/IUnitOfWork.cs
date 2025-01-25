using PSM.Domain.Categories;
using PSM.Domain.Products;

namespace PSM.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
