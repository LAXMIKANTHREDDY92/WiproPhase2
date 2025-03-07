using Microsoft.AspNetCore.Mvc;
using AspNetCoreMVC.Models;
using System.Collections.Generic;

namespace AspNetCoreMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductList1()
        {
            List<Product> products = new List<Product>
            {
                new Product { PCode = 101, Name = "Laptop", QtyInStock = 10, Details = "High-performance laptop", Price = 1200.99m },
                new Product { PCode = 102, Name = "Phone", QtyInStock = 25, Details = "Latest smartphone", Price = 799.99m },
                new Product { PCode = 103, Name = "Tablet", QtyInStock = 15, Details = "10-inch screen", Price = 499.99m },
                new Product { PCode = 104, Name = "Headphones", QtyInStock = 50, Details = "Noise-cancelling", Price = 149.99m },
                new Product { PCode = 105, Name = "Smartwatch", QtyInStock = 30, Details = "Water-resistant", Price = 199.99m }
            };

            ViewBag.Products = products;
            return View();  // ✅ Ensure this returns `View()`
        }
    }
}
