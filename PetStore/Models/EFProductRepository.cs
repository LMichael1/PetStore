using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        #region fields

        private ApplicationDbContext _context; 

        #endregion

        public IQueryable<Product> Products => _context.Products;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
