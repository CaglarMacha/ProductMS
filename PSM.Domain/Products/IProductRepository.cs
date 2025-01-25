using PSM.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<Product> GetProductWithCategoryAsync(Guid productId);
        Task<Product> GetProductByTitleAsync(string title);
    }
}
