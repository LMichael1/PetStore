using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.Infrastructure;
using PetStore.Models;
using PetStore.Models.ViewModels;

namespace PetStore.Controllers
{
    public class CartController : Controller
    {
        #region fields

        private IProductRepository _repository;
        private Cart _cart; 

        #endregion

        public CartController(IProductRepository repo, Cart cartService)
        {
            _repository = repo;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ID == productId);
            if (product != null)
            {
                _cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
                string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ID == productId);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}