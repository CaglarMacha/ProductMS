using PSM.Domain.Categories;
using PSM.Domain.Products;

namespace PSM.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<Product,Guid> Products { get; }
        //IRepository<Category, Guid> Categories { get; }
        Task<int> CompleteAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
