using PSM.Application.Contracts.Products;


namespace PSM.Application.Contracts
{
    public interface IProductAppService
    {
        Task<ProductDto> GetAsync(Guid id);
        //Task<List<TEntity>> GetListAsync();
        //Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ProductDto> CreateAsync(CreateProductDto entity);
        //Task<CreateProductDto> InsertAsync(Products entity);
        //Task<TEntity> UpdateAsync(TEntity entity);
        //Task DeleteAsync(TKey id);

    }
}
