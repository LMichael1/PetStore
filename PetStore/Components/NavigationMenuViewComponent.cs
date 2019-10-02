using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PetStore.Models;

namespace PetStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        #region fields

        private IProductRepository _repository; 

        #endregion

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
