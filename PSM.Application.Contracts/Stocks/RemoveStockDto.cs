using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Stocks
{
    public class RemoveStockDto
    {
        public int Quantity { get; set; }
        public StockActionTypes ActionTypes { get; set; }
        public Guid ProductId { get; set; }
    }
}
