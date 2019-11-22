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
        private int PageSize = 4;
        #endregion

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
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

        [HttpPut]
        public IActionResult Create(Сomment comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                _commentRepository.SaveComment(comment);

                return RedirectToAction("GetByProductId");
            }
            else
            {
                // there is something wrong with the data values
                return View(comment);
            }
        }

        [HttpPut]
        public IActionResult Edit(Сomment comment)
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