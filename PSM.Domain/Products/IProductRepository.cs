using PSM.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<Product> GetProductByTitleAsync(string title);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetFilteredProductsAsync(string? keyword = null, int? minStockQuantity = null, int? maxStockQuantity = null);
        Task<List<Product>> GetFilteredProductsAsync(Expression<Func<Product, bool>> predicate);
        Task<Product?> GetActiveProductAsync(Guid id);
        Task<Product?> GetProductWithStockAsync(Guid id);
    }
}
