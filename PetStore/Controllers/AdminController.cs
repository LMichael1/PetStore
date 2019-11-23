using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PetStore.Models.MongoDb;
using Microsoft.AspNetCore.Http;
using System;
using PetStore.Filters.FilterParameters;
using PetStore.Filters;
using PetStore.Models.ViewModels;

namespace PetStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ImagesDbContext _imagesDb;
        private IStockRepository _stockRepository;
        private IProductRepository _productRepository;
        private IFilterConditionsProducts _filterConditions;
        private int PageSize = 4;

        public AdminController(IProductRepository repo, IStockRepository stockRepo, ImagesDbContext context,
                    IFilterConditionsProducts filterConditions)
        {
            _productRepository = repo;
            _stockRepository = stockRepo;
            _imagesDb = context;
            _filterConditions = filterConditions;
        }

        [Authorize(Roles = "Admin, Manager")]
        public ViewResult Index(FilterParametersProducts filter, int productPage = 1)
        {
            ViewBag.Current = "Products";
            var stock = _stockRepository.StockItems;
            stock = _filterConditions.GetStockProducts(stock, filter);

            var paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = filter.Categories == null ?
                        stock.Count() :
                        stock.Where(e =>
                             filter.Categories.Contains(e.Product.Category)).Count()
            };

            return View(new AdminProductsListViewModel
            {
                Stock = stock
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = paging,
                CurrentFilter = filter
            });
        }

        public ViewResult SearchList(FilterParametersProducts filter, int productPage = 1)
        {
            var stock = _stockRepository.StockItems;
            stock = _filterConditions.GetStockProducts(stock, filter);

            var paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = filter.Categories == null ?
                        stock.Count() :
                        stock.Where(e =>
                             filter.Categories.Contains(e.Product.Category)).Count()
            };

            if (stock.Count() == 0)
            {
                TempData["message_search"] = $"Поиск не дал результатов";
            }

            return View(new AdminProductsListViewModel
            {
                Stock = stock
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = paging,
                CurrentFilter = filter,
                Categories = _stockRepository.StockItems
                    .Select(x => x.Product.Category)
                    .Distinct()
                    .OrderBy(x => x).ToList()
            });
        }

        [Authorize(Roles = "Admin, Manager")]
        public ViewResult Edit(int productId) =>
            View(_productRepository.Products
                .FirstOrDefault(p => p.ID == productId));

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    var imageName = DateTime.Now.ToString() + product.Name;
                    var image = await _imagesDb.StoreImage(product.Image.OpenReadStream(),
                                                            imageName);

                    product.ImageId = image;
                }

                _productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранен";

                if (_stockRepository.StockItems.FirstOrDefault(s => s.Product.ID == product.ID) == null)
                {
                    _stockRepository.SaveStockItem(new Stock { Product = product, Quantity = 0 });
                }

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
            Stock deletedStock = _stockRepository.DeleteStockItem(productId);
            Product deletedProduct = _productRepository.DeleteProduct(productId);

            if (deletedProduct != null && deletedStock != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult AddToStock(int stockId, int quantity)
        {
            var stock = _stockRepository.StockItems.FirstOrDefault(s => s.ID == stockId);

            stock.Quantity += quantity;
            _stockRepository.SaveStockItem(stock);

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

                Product product = _productRepository.Products.FirstOrDefault(p => p.ID == id);
                product.ImageId = image;

                _productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранен";
            }

            return RedirectToAction("Index");
        }
    }
}