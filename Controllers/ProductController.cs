using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalR101.Interfaces;
using signalR101.Models;

namespace signalR101.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IHubContext<SignalServer> context;

        public ProductController(IProductRepository productRepository, IHubContext<SignalServer> context)
        {
            this.productRepository = productRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(productRepository.All());
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            if (!ModelState.IsValid) return View(model);

            productRepository.Add(model);
            productRepository.SaveChanges();
            context.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            return View(productRepository.Find(id));
        }
        [HttpPost]
        public IActionResult EditProduct(Product model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = productRepository.Find(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.IsAvailable = model.IsAvailable;
            productRepository.Update(product);
            productRepository.SaveChanges();

            context.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult DeleteProduct(Guid productId)
        {
            var product = productRepository.Find(productId);
            productRepository.Delete(product);
            productRepository.SaveChanges();
            context.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction("index");
        }
    }
}