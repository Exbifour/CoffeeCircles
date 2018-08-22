using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public DateTime WorkingFrom { get; set; }
        public DateTime WorkingTo { get; set; }
        // Add photo property later.
    }
}
