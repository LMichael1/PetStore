using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public int PageSize = 4;

        #endregion

        public ProductController(IProductRepository repository, IStockRepository stockRepository, ImagesDbContext context)
        {
            _repository = repository;
            _stockRepository = stockRepository;
            _imagesDb = context;
        }

        public ViewResult List(string category, int productPage = 1)
        {
            var products = _stockRepository.StockItems
                    .Where(p => p.Quantity > 0)
                    .Select(p => p.Product)
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ID);

            var paging = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                        products.Count() :
                        products.Where(e =>
                            e.Category == category).Count()
            };

            return View(new ProductsListViewModel
            {
                Products = products
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = paging,
                CurrentCategory = category
            });
        }

        public async Task<ActionResult> GetImage(string id)
        {
            //if (true)
            //{
            //    return NotFound();
            //}
            var image = await _imagesDb.GetImage(id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image, "image/png");
        }
    }
}
