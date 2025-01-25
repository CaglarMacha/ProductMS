using PSM.Application.Contracts.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Stocks
{
    public interface IStockAppService
    {
        Task<StockDto> CreateAsync(CreateStockDto entity);
    }
}
