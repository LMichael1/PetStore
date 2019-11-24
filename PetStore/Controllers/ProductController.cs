using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Filters;
using PetStore.Filters.FilterParameters;
using PetStore.Models;
using PetStore.Models.MongoDb;
using PetStore.Models.ViewModels;

namespace PetStore.Controllers
{
    public class ProductController : Controller
    {
        #region fields

        private readonly ImagesDbContext _imagesDb;

        private IProductRepository _repository;

        private IStockRepository _stockRepository;

        private IProductExtendedRepository _productExtendedRepository;

        private IFilterConditionsProducts _filterConditions;

        public int PageSize = 4;

        #endregion

        public ProductController(IProductRepository repository,
                                IStockRepository stockRepository,
                                IProductExtendedRepository productExtendedRepository,
                                ImagesDbContext context,
                                IFilterConditionsProducts filterConditions)
        {
            _repository = repository;
            _stockRepository = stockRepository;
            _productExtendedRepository = productExtendedRepository;
            _imagesDb = context;
            _filterConditions = filterConditions;
        }

        public ViewResult List(FilterParametersProducts filter, int productPage = 1)
        {
            var products = _repository.Products;
            products = _filterConditions.GetProducts(products, filter);

            foreach (var p in products)
            {
                if (_stockRepository.StockItems.FirstOrDefault(pr => pr.Product == p && pr.Quantity > 0) != null)
                {
                    p.IsInStock = true;
                }
            }

            var paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = filter.Categories == null ?
                        products.Count() :
                        products.Where(e =>
                             filter.Categories.Contains(e.Category)).Count()
            };

            return View(new ProductsListViewModel
            {
                Products = products
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = paging,
                CurrentFilter = filter
            });
        }

        public ViewResult SearchList(FilterParametersProducts filter, int productPage = 1)
        {
            var products = _repository.Products;
            products = _filterConditions.GetProducts(products, filter);

            foreach (var p in products)
            {
                if (_stockRepository.StockItems.FirstOrDefault(pr => pr.Product == p && pr.Quantity > 0) != null)
                {
                    p.IsInStock = true;
                }
            }

            var paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = filter.Categories == null ?
                        products.Count() :
                        products.Where(e =>
                             filter.Categories.Contains(e.Category)).Count()
            };

            if (products.Count() == 0)
            {
                TempData["message_search"] = $"Поиск не дал результатов";
            }

            return View(new ProductsListViewModel
            {
                Products = products
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = paging,
                CurrentFilter = filter,
                Categories = _repository.Products
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x).ToList()
            });
        }

        public ViewResult Info(int productId)
        {
            var result = _productExtendedRepository.ProductsExtended
                    .FirstOrDefault(p => p.Product.ID == productId);

            if (result == null)
            {
                RedirectToAction("List");
            }

            if (_stockRepository.StockItems.FirstOrDefault(p => p.Product.ID == result.Product.ID && p.Quantity > 0) != null)
            {
                result.Product.IsInStock = true;
            }

            return View(result);
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
    }
}
