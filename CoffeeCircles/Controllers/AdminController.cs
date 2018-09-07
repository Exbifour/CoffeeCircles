using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCircles.Data;
using CoffeeCircles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeCircles.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _env;

        public AdminController(ApplicationDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult AddProduct()
        {
            return RedirectToAction("EditProduct", new Product());
        }

        private List<SelectListItem> CollectProductTypes()
        {
            return _db.ProductTypes
                   .Select(pt => new SelectListItem()
                   {
                       Value = pt.ProductTypeId.ToString(),
                       Text = pt.Name
                   })
                   .ToList();
        }

        [HttpGet]
        public IActionResult EditProduct(Product product)
        {
            ViewBag.ProductTypes = CollectProductTypes();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProductAsync(IFormFile photo, Product product)
        {
            if (ModelState.IsValid)
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

                return RedirectToAction("Products", "Home");
            }

            ViewBag.ProductTypes = CollectProductTypes();
            return RedirectToAction("EditProduct", product);
        }
    }
}