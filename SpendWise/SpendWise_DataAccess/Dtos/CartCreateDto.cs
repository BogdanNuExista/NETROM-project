using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class CartCreateDto
    {
        public DateTime Date { get; set; }

        public List<CategorisedProductsDto> CategoryProducts { get; set; } = new List<CategorisedProductsDto>();
    }
}
