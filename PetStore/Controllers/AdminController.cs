using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PetStore.Models.MongoDb;
using Microsoft.AspNetCore.Http;
using System;

namespace PetStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ImagesDbContext _imagesDb;
        private IProductRepository _repository;

        public AdminController(IProductRepository repo, ImagesDbContext context)
        {
            _repository = repo;
            _imagesDb = context;
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
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image!=null)
                {
                    var imageName = DateTime.Now.ToString() + product.Name;
                    var image = await _imagesDb.StoreImage(product.Image.OpenReadStream(), 
                                                            imageName);

                    product.ImageId = image;
                }

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

        public async Task<ActionResult> GetImage(string id)
        {
            var image = await _imagesDb.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }

        [HttpPost]
        public async Task<ActionResult> AttachImage(int id, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var image = await _imagesDb.StoreImage(uploadedFile.OpenReadStream(), uploadedFile.FileName);

                Product product = _repository.Products.FirstOrDefault(p => p.ID == id);
                product.ImageId = image;

                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранен";
            }

            return RedirectToAction("Index");
        }
    }
}