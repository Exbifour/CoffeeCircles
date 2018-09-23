using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCircles.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [StringLength(maximumLength: 30, MinimumLength = 2, 
            ErrorMessage = "Name should not be shorter that 2 symbols and not longer than 30.")]
        public string Nickname { get; set; }

        public Moderator Moderator { get; set; }
    }
}
