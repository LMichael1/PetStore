using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public interface ICommentRepository
    {
        IQueryable<Сomment> Сomment { get; }
        void SaveComment(Сomment comment);
        Сomment DeleteComment(int CommentID);
    }
}
