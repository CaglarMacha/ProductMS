using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Categories
{
    public class CategoryManager
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<Category> CreateAsync([NotNull] string name)
        {
            var existingCategory = await categoryRepository.GetCategoryByNameAsync(name);
            if (existingCategory == null)
            {
                var newCategory = await categoryRepository.CreateAsync(new Category { Id = Guid.NewGuid(), Name = name });
                return newCategory;
            }
            else
                return existingCategory;
        }
    }
}
