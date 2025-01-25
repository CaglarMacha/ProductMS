using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Products
{
    public class GetFilteredProductsDto
    {
        public string Title { get; set; }
        public int? MaxStock { get; set; }
        public int? MinStock { get; set; }
    }
}
