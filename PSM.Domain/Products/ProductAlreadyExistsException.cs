using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Products
{
    public class ProductAlreadyExistsException : BusinessException
    {
        public ProductAlreadyExistsException(string message):base(message: message)
        {
        }
    }
}
