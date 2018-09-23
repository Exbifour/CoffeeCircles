﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCircles.Data;
using CoffeeCircles.Models;
using CoffeeCircles.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            return View("EditProduct", new Product());
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
                if (product.ProductId <= 0)
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
            var prod = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (prod.PhotoRef != "/Images/no-image.png" && System.IO.File.Exists(_env.WebRootPath + prod.PhotoRef))
            {
                System.IO.File.Delete(_env.WebRootPath + prod.PhotoRef);
            }
            _db.Products.Remove(prod);
            _db.SaveChanges();
            return RedirectToAction("Products", "Home");
        }

        public IActionResult CreateShop()
        {
            return View("EditShop", new Shop());
        }

        public IActionResult EditShop(int id)
        {
            Shop shop = _db.Shops.FirstOrDefault(s => s.ShopId == id);
            return View(shop);
        }

        [HttpPost]
        public IActionResult CreateEditShop(IFormFile photo, Shop shop)
        {
            if (ModelState.IsValid)
            {
                if (shop.ShopId <= 0)
                {
                    _db.Shops.Add(shop);
                    _db.SaveChanges();
                    shop.PhotoRef = "/Images/no-image.png";
                }
                else
                {
                    _db.Shops.Update(shop);
                }

                if (photo != null)
                {
                    string photoPath = shop.PhotoRef = "/Images/Shops/"
                        + shop.ShopId
                        + photo.FileName.Substring(photo.FileName.LastIndexOf('.'));
                    using (FileStream fs = new FileStream(_env.WebRootPath + photoPath, FileMode.Create))
                    {
                        photo.CopyTo(fs);
                    };
                }

                _db.SaveChanges();

                return RedirectToAction("Shops", "Home");
            }

            return View("EditShop");
        }

        public IActionResult RemoveShop(int id)
        {
            var shop = _db.Shops.FirstOrDefault(s => s.ShopId == id);
            if (shop.PhotoRef != "/Images/no-image.png" && System.IO.File.Exists(_env.WebRootPath + shop.PhotoRef))
            {
                System.IO.File.Delete(_env.WebRootPath + shop.PhotoRef);
            }
            _db.Shops.Remove(shop);
            _db.SaveChanges();
            return RedirectToAction("Shops", "Home");
        }

        public IActionResult Moderators(string searchForUser)
        {
            ModeratorsViewModel moderators = new ModeratorsViewModel()
            {
                Shops = _db.Shops.Include(s => s.Moderators).ThenInclude(m => m.User).ToList(),
                SearchResult = searchForUser == null ? null : _db.Users.Where(u => u.UserName == searchForUser).ToList()
            };
            
            return View(moderators);
        }

        public IActionResult EditModerator(string userId)
        {
            Moderator mod = _db.Moderators.FirstOrDefault(m => m.UserId == userId);
            if (mod == null)
            {
                mod = new Moderator() { UserId = userId };
            }
            ViewBag.Shops = _db.Shops;
            return View(mod);
        }

        [HttpPost]
        public IActionResult CreateEditModerator(Moderator mod)
        {
            if(ModelState.IsValid)
            {
                if (_db.Moderators.Contains(mod))
                {
                    _db.Moderators.Update(mod);
                }
                else
                {
                    _db.Moderators.Add(mod);
                }
                _db.SaveChanges();

                return RedirectToAction("Moderators");
            }
            return View("EditModerator");
        }

        public IActionResult RemoveModerator(string userId)
        {
            Moderator mod = _db.Moderators.FirstOrDefault(m => m.UserId == userId);
            _db.Moderators.Remove(mod);
            _db.SaveChanges();

            return RedirectToAction("Moderators");
        }
    }
}