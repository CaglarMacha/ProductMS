using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Products
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } 
    }
}
