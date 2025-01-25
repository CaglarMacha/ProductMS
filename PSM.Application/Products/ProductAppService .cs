using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PSM.Application.Contracts.Products;
using PSM.Domain.Shared;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper.Internal.Mappers;

namespace PSM.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductManager productManager;

        public ProductAppService(IProductRepository productRepository, IUnitOfWork unitOfWork, ProductManager productManager, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.productManager = productManager;
            this.mapper = mapper;
        }
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            unitOfWork.BeginTransaction();
            var product = await productManager.CreateAsync(input.Title, input.Category, input.Description);

           
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
        public async Task<List<ProductDto>> GetFilteredProductsAsync(string title, int? minStock, int? maxStock)
        {
            var query = await productRepository.GetFilteredProductsAsync(title, minStock, maxStock);

            return mapper.Map<List<Product>, List<ProductDto>>(query);
        }
        public async Task<ProductDto> GetProductWithStockAsync(Guid id)
        {
            var query = await productRepository.GetProductWithStockAsync(id);

            return mapper.Map<Product, ProductDto>(query);
        }
        public async Task<ProductDto> DeleteAsync(Guid id)
        {
            unitOfWork.BeginTransaction();
            var data = await productManager.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();
            unitOfWork.Dispose();
            return mapper.Map<ProductDto>(data);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var data = await productRepository.GetAsync(id);
            return mapper.Map<ProductDto>(data);
        }

        public async Task<List<ProductDto>> GetListAsync()
        {
            var data = await productRepository.GetListAsync();
            return mapper.Map<List<ProductDto>>(data);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto entity)
        {
            unitOfWork.BeginTransaction();
            var existingProduct = await productRepository.GetAsync(id);
            if (existingProduct == null)
            {
                throw new BusinessException(message: "Not Found");
            }

            var updatedProduct = await productManager.UpdateAsync(id, entity.Title, entity.CategoryName, entity.Description);
            await unitOfWork.CommitTransactionAsync();
            unitOfWork.Dispose();

            return mapper.Map<ProductDto>(updatedProduct);
        }

        //public Task<List<ProductDto>> GetListAsync(Expression<Func<ProductDto, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
