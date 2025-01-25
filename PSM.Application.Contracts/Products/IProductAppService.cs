namespace PSM.Application.Contracts.Products
{
    public interface IProductAppService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<List<ProductDto>> GetListAsync();
        Task<List<ProductDto>> GetFilteredProductsAsync(string title, int? minStock, int? maxStock);
        Task<ProductDto> CreateAsync(CreateProductDto entity);
        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto entity);
        Task<ProductDto> DeleteAsync(Guid id);
        Task<ProductDto> GetProductWithStockAsync(Guid id);
        //Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<CreateProductDto> InsertAsync(Products entity);


    }
}
