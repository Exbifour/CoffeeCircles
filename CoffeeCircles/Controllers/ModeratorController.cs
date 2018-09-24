using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeCircles.Data;
using CoffeeCircles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCircles.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class ModeratorController : Controller
    {
        readonly ApplicationDbContext _db;

        public ModeratorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult HideProduct(int shopId, int prodId)
        {
            var record = new ShopUnavailableList()
            {
                ShopId = shopId,
                ProductId = prodId
            };
            _db.ShopUnavailableLists.Add(record);
            _db.SaveChanges();
            return RedirectToAction("ShopDetails", "Home", new { id = shopId });
        }

        public IActionResult ShowProduct(int shopId, int prodId)
        {
            var record = _db.ShopUnavailableLists.FirstOrDefault(r => r.ShopId == shopId && r.ProductId == prodId);
            _db.ShopUnavailableLists.Remove(record);
            _db.SaveChanges();
            return RedirectToAction("ShopDetails", "Home", new { id = shopId });
        }
    }
}