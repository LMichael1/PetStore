using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Filters.FilterParameters
{
    public class FilterParametersProducts
    {
        public string Name { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Category { get; set; }
        public List<string> Categories { get; set; } 
    }
}
