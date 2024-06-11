using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
