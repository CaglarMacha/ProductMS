using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Stocks
{
    public class CreateStockDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
