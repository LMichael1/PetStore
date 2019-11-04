﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.Filters.FilterParameters;
using PetStore.Models;

namespace PetStore.Filters
{
    public class FilterConditionsProducts : IFilterConditionsProducts
    {
        public IQueryable<Product> GetProducts(IQueryable<Product> products, FilterParametersProducts filter)
        {
            products = products.Where(p => filter.Categories == null || filter.Categories.Contains(p.Category));

            if (!String.IsNullOrEmpty(filter.Name))
            {
                products = products.Where(c => c.Name.ToUpper().Contains(filter.Name.ToUpper()));
            }

            if (filter.MinPrice > 0)
            {
                products = products.Where(c => c.Price >= filter.MinPrice);
            }

            if (filter.MaxPrice > 0)
            {
                products = products.Where(c => c.Price <= filter.MaxPrice);
            }

            return products;
        }
    }
}