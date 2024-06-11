using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    }
}
