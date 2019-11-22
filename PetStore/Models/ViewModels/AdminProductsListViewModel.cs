﻿using PetStore.Filters.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models.ViewModels
{
    public class AdminProductsListViewModel
    {
        #region properties

        public IEnumerable<Stock> Stock { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public FilterParametersProducts CurrentFilter { get; set; }
        public List<string> Categories { get; set; }

        #endregion
    }
}
