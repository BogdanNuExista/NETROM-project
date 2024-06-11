using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class ScannedProducts
    {
        [JsonProperty("nume produs")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("cantitate")]
        public int Quantity { get; set; }

        [JsonProperty("pret")]
        public double Price { get; set; }
    }
}
