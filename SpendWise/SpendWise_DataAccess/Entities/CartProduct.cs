using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Entities
{
    public class CartProduct
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public virtual Cart Cart { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
