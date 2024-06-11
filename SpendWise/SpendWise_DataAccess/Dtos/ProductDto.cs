using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class ProductDto
    { 
        public string Name { get; set; } = string.Empty;

        public IEnumerable<int> CategoryId { get; set; } = new List<int>();
    }
}
