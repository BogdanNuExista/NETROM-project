using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class TotalPriceCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
    }

}
