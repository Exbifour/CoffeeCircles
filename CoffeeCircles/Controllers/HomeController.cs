using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeCircles.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using CoffeeCircles.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCircles.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _env;

        public HomeController(ApplicationDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
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

        [HttpGet]
        public IActionResult EditProduct()
        {
            ViewBag.ProductTypes = _db.ProductTypes
                   .Select(pt => new SelectListItem()
                   {
                       Value = pt.ProductTypeId.ToString(),
                       Text = pt.Name
                   })
                   .ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(IFormFile photo, Product product)
        {
            if(ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                if (photo == null)
                {
                    product.PhotoRef = "/Images/no-image.png";
                }
                else
                {
                    string path = "/Images/Products/"
                        + product.ProductId
                        + photo.FileName.Substring(photo.FileName.LastIndexOf('.'));
                    using (FileStream fs = new FileStream(_env.WebRootPath + path, FileMode.Create))
                    {
                        await photo.CopyToAsync(fs);
                    }
                    product.PhotoRef = path;
                }

                await _db.SaveChangesAsync();

                return RedirectToAction("Products");
            }

            ViewBag.ProductTypes = _db.ProductTypes
                   .Select(pt => new SelectListItem()
                   {
                       Value = pt.ProductTypeId.ToString(),
                       Text = pt.Name
                   })
                   .ToList();
            return View("EditProduct");
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
