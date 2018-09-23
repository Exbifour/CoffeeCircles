using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeCircles.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        [Required]
        [MaxLength(256)]
        public string City { get; set; }
        [Required]
        [MaxLength(256)]
        public string Street { get; set; }
        [Required]
        [MaxLength(256)]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan WorkingFrom { get; set; }
        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan WorkingTo { get; set; }
        public string PhotoRef { get; set; }

        // Navigation properties.
        public virtual ICollection<ShopUnavailableList> UnavaliableList { get; private set; }
        public virtual ICollection<Moderator> Moderators { get; set; }
    }
}
