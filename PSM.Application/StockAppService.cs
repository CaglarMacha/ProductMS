using PSM.Application.Contracts.Products;
using PSM.Application.Contracts.Stocks;
using PSM.Domain.Products;
using PSM.Domain.Shared;
using PSM.Domain.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application
{
    public class StockAppService: IStockAppService
    {
        private readonly StockManager stockManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly IStockRepository stockRepository;
        public StockAppService(StockManager stockManager, IUnitOfWork unitOfWork, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.stockManager = stockManager;
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }
        public async Task<StockDto> CreateAsync(CreateStockDto input)
        {
            var existingProduct = await productRepository.GetAsync(input.ProductId);
            if (existingProduct == null)
                throw new Exception("Product Already Exists");
           
            var stock = await stockManager.CreateAsync(input.ProductId, input.Quantity);

            existingProduct.IsActive = true;
            existingProduct.SetQuantity(input.Quantity);
            unitOfWork.BeginTransaction();
            await stockRepository.CreateAsync(stock);
            await productRepository.UpdateAsync(existingProduct);
            await unitOfWork.CommitTransactionAsync();
            unitOfWork.Dispose();
            return new StockDto
            {
                Id = stock.Id,
                Quantity = existingProduct.StockQuantity
            };
        }
    }
}
