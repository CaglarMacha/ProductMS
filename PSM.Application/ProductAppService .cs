using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PSM.Application.Contracts.Products;
using PSM.Application.Contracts;
using PSM.Domain.Shared;

namespace PSM.Application
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository productRepository;

        private readonly IUnitOfWork unitOfWork;
        private readonly ProductManager productManager;

        public ProductAppService(IProductRepository productRepository, IUnitOfWork unitOfWork, ProductManager productManager)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.productManager = productManager;
        }
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = await productManager.CreateAsync(input.Title, input.Category, input.Description);

            unitOfWork.BeginTransaction();
            await productRepository.CreateAsync(product);
            await unitOfWork.CommitTransactionAsync();
            unitOfWork.Dispose();
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var data =  await productRepository.GetAsync(id);
        }

        public Task<List<ProductDto>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetListAsync(Expression<Func<ProductDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> InsertAsync(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> UpdateAsync(ProductDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
