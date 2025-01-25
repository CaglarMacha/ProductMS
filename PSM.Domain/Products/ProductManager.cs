using PSM.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Products
{
    public class ProductManager
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly CategoryManager categoryManager;
        public ProductManager(IProductRepository productRepository, CategoryManager categoryManager)
        {
            this.productRepository = productRepository;
            this.categoryManager = categoryManager;
        }
        public async Task<Product> CreateAsync([NotNull] string title, string categoryName, string Description = null)
        {
            if (await CheckProductExistAsync(title))
                throw new ProductAlreadyExistsException("Product Already Exists" + " " + title);
            var category = await categoryManager.CreateAsync(categoryName);
            var newProduct = new Product(
                Guid.NewGuid(),
                title,
                category.Id,
                Description
            );
            newProduct.Category = category;
            return newProduct;
        }
        public async Task<Product> DeleteAsync(Guid id)
        {
            var existingProduct = await productRepository.GetAsync(id);
            if (existingProduct == null) throw new Exception("Not Found");
            if (existingProduct.StockQuantity > 0)
            {
                throw new Exception("Having Stock Quantity Product Can Not Delete");
            }
            else
            {
                await productRepository.DeleteAsync(existingProduct);
            }
            return existingProduct;
        }

        private async Task<bool> CheckProductExistAsync(string title)
        {
            var existingProduct = await productRepository.GetProductByTitleAsync(title);
            if (existingProduct != null)
                return true;
            else
                return false;
        }
    }
}
