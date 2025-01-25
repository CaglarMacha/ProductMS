using PSM.Domain.Products;
using PSM.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Categories
{
    public interface ICategoryRepository: IRepository<Category, Guid>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
    }
}
