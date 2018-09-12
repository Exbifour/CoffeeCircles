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
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _env;

        public AdminController(ApplicationDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult CreateProduct()
        {
            ViewBag.Producttypes = CollectProductTypes();
            return View("EditProduct");
        }

        public IActionResult EditProduct(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.ProductTypes = CollectProductTypes();
            return View(product);
        }

        [HttpPost]
        public IActionResult CreateEditProduct(IFormFile photo, Product product)
        {
            if (ModelState.IsValid)
            {
                if(product.ProductId <= 0)
                {
                    _db.Products.Add(product);
                    _db.SaveChanges();
                    product.PhotoRef = "/Images/no-image.png";
                }
                else
                {
                    _db.Products.Update(product);
                }

                if (photo != null)
                {
                    string photoPath = product.PhotoRef = "/Images/Products/"
                        + product.ProductId
                        + photo.FileName.Substring(photo.FileName.LastIndexOf('.'));
                    using (FileStream fs = new FileStream(_env.WebRootPath + photoPath, FileMode.Create))
                    {
                        photo.CopyTo(fs);
                    };
                }

                _db.SaveChanges();

                return RedirectToAction("Products", "Home");
            }

            ViewBag.ProductTypes = CollectProductTypes();
            return View("EditProduct");
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

        public IActionResult RemoveProduct(int id)
        {
            _db.Remove(_db.Products.FirstOrDefault(p => p.ProductId == id));
            _db.SaveChanges();
            return RedirectToAction("Products", "Home");
        }
        
    }
}