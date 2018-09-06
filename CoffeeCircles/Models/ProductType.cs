using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Cn't be longer than 30 symbols")]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; private set; }
    }
}
