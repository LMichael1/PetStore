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
        private readonly IProductExtended _productExtendedRepository;
        private int PageSize = 4;
        #endregion

        public CommentController(ICommentRepository commentRepository, IProductExtended productExtendedRepository)
        {
            _commentRepository = commentRepository;
            _productExtendedRepository = productExtendedRepository;
        }

        public ViewResult GetByProductId(int id, int commentPage = 1)
        {
            var comments = _commentRepository.Сomment.Where(p => p.Product.ID == id);

            if(comments.Count() == 0)
            {
                TempData["message_search"] = $"Поиск не дал результатов";
            }

            var paging = new PagingInfo
            {
                CurrentPage = commentPage,
                ItemsPerPage = PageSize,
                TotalItems = comments.Count()
            };

            var commentViewModel = new CommentViewModel()
            {
                Comments = comments,
                PagingInfo = paging
            };

            return View(commentViewModel);
        }

        public ViewResult Create() => View("Edit", new Сomment());

        [HttpPost]
        public IActionResult Create(Сomment comment, int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                _commentRepository.SaveComment(comment);

                _productExtendedRepository.ProductExtended.FirstOrDefault(p => p.Product.ID == productId)
                    .Comments.Add(comment);
                _productExtendedRepository.SaveChanges();

                return RedirectToAction("GetByProductId");
            }
            else
            {
                // there is something wrong with the data values
                return View(comment);
            }
        }

        public ViewResult Edit(int commentId) => 
            View(_commentRepository.Сomment.FirstOrDefault(p => p.ID == commentId));

        [HttpPut]
        public IActionResult Edit(Сomment comment, int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }

            if(User.Identity.Name != comment.UserName)
            {
                TempData["message_rights"] = $"Пользователь не имеет права редактировать комментарий";
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
                // there is something wrong with the data values
                return View(comment);
            }
        }

        [HttpPost]
        public IActionResult Delete(int commentId)
        {
            var comment = _commentRepository.Сomment.FirstOrDefault(p => p.ID == commentId);

            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }
            else if (User.Identity.Name != comment.UserName)
            {
                TempData["message_rights"] = $"Пользователь не имеет права удалять комментарий";
            }
            else
            {
                TempData["message"] = $"{comment.Message} was deleted";
            }

            return RedirectToAction("GetByProductId");
        }
    }
}