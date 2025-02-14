using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
