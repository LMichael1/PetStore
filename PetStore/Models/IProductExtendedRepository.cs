﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public interface IProductExtendedRepository
    {
        IQueryable<ProductExtended> ProductExtended { get;}
        void SaveProductExtended(ProductExtended productExtended);
        ProductExtended DeleteProductExtended(int productExtendedID);
    }
}