using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models.ViewModels
{
    public class ModeratorsViewModel
    {
        public List<ApplicationUser> SearchResult { get; set; }
        public List<Shop> Shops { get; set; }
    }
}
