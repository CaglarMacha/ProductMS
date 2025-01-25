using PSM.Domain.Categories;
using PSM.Domain.Stocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<Stock> CreateAsync([NotNull] Guid productId, int quantity)
        {
            var newStock = new Stock(
                productId,
                quantity
            );
            newStock.StockActionTypes = StockActionTypes.Add;
            return newStock;
        }
        //public async Task<Stock> UpdateAsync([NotNull] Guid productId, int quantity,StockActionTypes stockActionType)
        //{
        //    var product = await productRepository.GetAsync(productId);
        //    switch (stockActionType)
        //    {
        //        case StockActionTypes.Add:
        //            return await CreateAsync(productId, quantity);
        //        case StockActionTypes.Remove:
        //            if (product.StockQuantity <= quantity)
        //                throw new ArgumentException();
        //            return await RemoveAsync(productId, quantity);
        //        default:
        //            throw new InvalidOperationException("Invalid stock action type");
        //    }
        //}
        public async Task<Stock> RemoveStockAsync([NotNull] Guid productId, int quantity)
        {
            var product = await productRepository.GetAsync(productId); // Bunu kaldır defalarca db giden yerleri gözden geçir
            if (product.StockQuantity <= quantity)
                throw new ArgumentException();
            var newStock = new Stock(
                productId,
                quantity
            );
            newStock.StockActionTypes = StockActionTypes.Remove;
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
