using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        // Add Phoro property later.
    }
}
