using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.Models;

namespace PetStore.Models.ViewModels
{
    public class CommentViewModel
    {
        public IEnumerable<Сomment> Comments { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
