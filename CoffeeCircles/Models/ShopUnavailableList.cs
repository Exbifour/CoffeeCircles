using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class ShopUnavailableList
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
