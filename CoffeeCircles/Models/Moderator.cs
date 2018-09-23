using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class Moderator
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        public int ShopId { get; set; }

        // Navigation properties.
        public ApplicationUser User { get; set; }
        public Shop Shop { get; set; }
    }
}
