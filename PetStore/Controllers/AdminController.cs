using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PetStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        [Authorize(Roles = "Admin, Manager")]
        public ViewResult Index()
        {
            ViewBag.Current = "Products";

            return View(_repository.Products);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ViewResult Edit(int productId) =>
            View(_repository.Products
                .FirstOrDefault(p => p.ID == productId));

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранен";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = _repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}