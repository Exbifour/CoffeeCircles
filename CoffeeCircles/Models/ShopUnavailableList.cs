using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class ShopUnavailableList
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }

        // Navigation properties.
        public virtual Shop Shop { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}