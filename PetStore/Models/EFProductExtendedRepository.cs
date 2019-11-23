using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public class EFProductExtendedRepository : IProductExtendedRepository
    {
        #region private
        private ApplicationDbContext _context;
        #endregion

        public IQueryable<ProductExtended> ProductExtended => _context.ProductExtendeds;

        public EFProductExtendedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductExtended DeleteProductExtended(int productExtendedID)
        {
            ProductExtended dbEntry = _context.ProductExtendeds
            .FirstOrDefault(p => p.ID == productExtendedID);
            if (dbEntry != null)
            {
                _context.ProductExtendeds.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProductExtended(ProductExtended productExtended)
        {
            if (productExtended.ID == 0)
            {
                _context.ProductExtendeds.Add(productExtended);
            }
            else
            {
                ProductExtended dbEntry = _context.ProductExtendeds
                    .FirstOrDefault(p => p.ID == productExtended.ID);
                if (dbEntry != null)
                {
                    dbEntry.Comments = productExtended.Comments;
                    dbEntry.Product = productExtended.Product;
                }
            }
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
