using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeCircles.Models;
using Microsoft.AspNetCore.Http;
using CoffeeCircles.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CoffeeCircles.Models.ViewModels;

namespace CoffeeCircles.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            ViewBag.Products = _db.Products.Include(p => p.ProductType);
            return View();
        }

        public IActionResult Shops()
        {
            ViewBag.Shops = _db.Shops;
            return View();
        }
        
        public IActionResult ShopDetails(int id)
        {
            ShopDetailsViewModel shopDetails = new ShopDetailsViewModel();
            shopDetails.Shop = _db.Shops.FirstOrDefault(s => s.ShopId == id);
            shopDetails.UnavailableProducts = _db.ShopUnavailableLists.Where(list => list.ShopId == id)
                .Include(list => list.Product)
                .Select(list => list.Product)
                .ToList();
            shopDetails.AvaliableProducts = _db.Products.Except(shopDetails.UnavailableProducts).ToList();

            return View(shopDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
