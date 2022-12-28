using AutoMapper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;
using FA.JustBlog.Utility;
using FA.JustBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Comment? comment = unitOfWork.CommentRepository.GetEntityById(id);

            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = mapper.Map<CommentViewModel>(comment);

            return View(commentViewModel);
        }

        //Get
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult Create(int postId)
        {
            Comment comment = new();
            comment.PostId = postId;

            CommentViewModel commentViewModel = mapper.Map<CommentViewModel>(comment);

            return View(commentViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult Create(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = mapper.Map<Comment>(commentViewModel);

                unitOfWork.CommentRepository.Create(comment);
                unitOfWork.SaveChanges();

                TempData["success"] = "Create successfully!";
                return RedirectToAction("Index", "Post");
            }

            TempData["error"] = "Create fail!";
            return RedirectToAction("Index", "Post");
        }


        //Get
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var comment = unitOfWork.CommentRepository.GetEntityById(id);
            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = mapper.Map<CommentViewModel>(comment);

            return View(commentViewModel);
        }

        //PUT
        [HttpPut]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Update(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = mapper.Map<Comment>(commentViewModel);

                unitOfWork.CommentRepository.Update(comment);
                unitOfWork.SaveChanges();

                TempData["success"] = "Create successfully!";
                return RedirectToAction("Index", "Post");
            }

            TempData["error"] = "Create fail!";

            return RedirectToAction("Index", "Post");
        }

        //Get
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var comment = unitOfWork.CommentRepository.GetEntityById(id);
            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = mapper.Map<CommentViewModel>(comment);

            return View(commentViewModel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = unitOfWork.CommentRepository.GetEntityById(id);

            if (comment == null)
            {
                TempData["error"] = "Delete fail!";
                return NotFound();
            }

            unitOfWork.CommentRepository.Delete(comment);
            unitOfWork.SaveChanges();
            TempData["success"] = "Delete Successfully!";

            return RedirectToAction("Index", "Post");
        }
    }
}
