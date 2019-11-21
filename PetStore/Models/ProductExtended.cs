using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetStore.Models;

namespace PetStore.Models
{
    public class ProductExtended
    {
        [BindNever]
        public int ID { get; set; }
        public Product Product { get; set; }
        public ICollection<Сomment> Comments { get; set; }
    }
}
