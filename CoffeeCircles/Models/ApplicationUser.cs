using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [StringLength(maximumLength: 30, MinimumLength = 2, 
            ErrorMessage = "Name should not be shorter that 2 symbols and not longer than 30.")]
        public string Nickname { get; set; }

        //// Navigation properties.
        //public virtual ICollection<ShopUnavailableList> UnavaliableList { get; private set; }
    }
}
