using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using PetStore.Models.ViewModels;

namespace PetStore.Controllers
{
    public class CommentController : Controller
    {
        #region private 
        private readonly ICommentRepository _commentRepository;
        private readonly IProductExtendedRepository _productExtendedRepository;
        private int PageSize = 4;
        #endregion

        public CommentController(ICommentRepository commentRepository, IProductExtendedRepository productExtendedRepository)
        {
            _commentRepository = commentRepository;
            _productExtendedRepository = productExtendedRepository;
        }

        //public ViewResult GetByProductId(int id, int commentPage = 1)
        //{
        //    var comments = _commentRepository.Сomment.Where(p => p.Product.ID == id);

        //    if(comments.Count() == 0)
        //    {
        //        TempData["message_search"] = $"Поиск не дал результатов";
        //    }

        //    var paging = new PagingInfo
        //    {
        //        CurrentPage = commentPage,
        //        ItemsPerPage = PageSize,
        //        TotalItems = comments.Count()
        //    };

        //    var commentViewModel = new CommentViewModel()
        //    {
        //        Comments = comments,
        //        PagingInfo = paging
        //    };

        //    return View(commentViewModel);
        //}

        public ViewResult Create(int productId) => View(new CommentViewModel { ProductId = productId });

        [HttpPost]
        public IActionResult Create(CommentViewModel commentModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Message = commentModel.Message,
                    Rating = commentModel.Rating,
                    Time = DateTime.Now,
                    UserName = User.Identity.Name
                };

                _commentRepository.SaveComment(comment);

                _productExtendedRepository.ProductExtended.FirstOrDefault(p => p.Product.ID == commentModel.ProductId)
                    .Comments.Add(comment);
                _productExtendedRepository.SaveChanges();

                return RedirectToAction("GetByProductId");
            }
            else
            {
                // there is something wrong with the data values
                return View(commentModel);
            }
        }

        public ViewResult Edit(int commentId) => 
            View(_commentRepository.Сomment.FirstOrDefault(p => p.ID == commentId));

        [HttpPut]
        public IActionResult Edit(Comment comment, int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }

            if(User.Identity.Name != comment.UserName)
            {
                TempData["message"] = $"Пользователь не имеет права редактировать комментарий";
            }

            if (ModelState.IsValid)
            {
                _commentRepository.SaveComment(comment);

                var repositoryComment = _productExtendedRepository.ProductExtended.FirstOrDefault(p => p.Product.ID == productId)
                    .Comments.FirstOrDefault(p => p.ID == comment.ID);
                repositoryComment.Message = comment.Message;
                repositoryComment.Rating = comment.Rating;
                repositoryComment.Time = DateTime.Now;
                _productExtendedRepository.SaveChanges();

                return RedirectToAction("GetByProductId");
            }
            else
            {
                return View(comment);
            }
        }

        [HttpPost]
        public IActionResult Delete(int commentId, int productId)
        {
            var comment = _commentRepository.DeleteComment(commentId);
            _productExtendedRepository.ProductExtended.FirstOrDefault(p => p.Product.ID == productId)
                .Comments.Remove(comment);

            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }
            else if (User.Identity.Name != comment.UserName || !User.IsInRole("Admin"))
            {
                TempData["message"] = $"Пользователь не имеет права удалять комментарий";
            }
            else
            {
                TempData["message"] = $"Комментарий удален";
            }

            return RedirectToAction("GetByProductId");
        }
    }
}