using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "SmallMoney")]
        public decimal Price { get; set; }
        [MaxLength(400, ErrorMessage = "Description should not be longer than 400 symbols")]
        public string Description { get; set; }
        public string PhotoRef { get; set; }

        // Foreign key.
        [Required]
        public int ProductTypeId { get; set; }

        // Navigation properties.
        public virtual ProductType ProductType { get; set; }
    }
}
