using PSM.Domain.Entities;
using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Stocks
{
    public class Stock : AuditedAggregateRoot<Guid>
    {
        public Stock()
        {
            
        }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Stock(Guid productId, int quantity)
        {
            ProductId = productId;
            SetQuantity(quantity);
        }
        private void SetQuantity([NotNull] int quantity)
        {
            if (quantity >= StockConsts.MinStockConst)
                Quantity = quantity;
        }
    }
}
