using CoffeeCircles.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCircles.Models.ViewModels
{
    public class ShopDetailsViewModel
    {
        public Shop Shop { get; set; }
        public List<Product> AvaliableProducts { get; set; }
        public List<Product> UnavailableProducts { get; set; }
    }
}
