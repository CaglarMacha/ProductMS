using PSM.Domain.Categories;
using PSM.Domain.Stocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Products
{
    public class StockManager
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly CategoryManager categoryManager;
        public StockManager(IProductRepository productRepository, CategoryManager categoryManager)
        {
            this.productRepository = productRepository;
            this.categoryManager = categoryManager;
        }
        public async Task<Stock> CreateAsync([NotNull] Guid ProductId, int Quantity)
        {
            var newStock = new Stock(
                ProductId,
                Quantity
            );
            return newStock;
        }

        private async Task<bool> CheckProductExistAsync(Guid id)
        {
            var existingProduct = await productRepository.GetAsync(id);
            if (existingProduct != null)
                return true;
            else
                return false;
        }
    }
}
