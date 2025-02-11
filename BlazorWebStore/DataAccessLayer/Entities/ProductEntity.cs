using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; } = [];

        public virtual List<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    }
}
