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
    public class TagController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagController(IUnitOfWork unitOfWork, IMapper mapper)
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
            Tag? tag = unitOfWork.TagRepository.GetEntityById(id);
            if (tag == null)
                return NotFound();

            TagViewModel tagViewModel = mapper.Map<TagViewModel>(tag);

            return View(tagViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Create(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                tagViewModel.UrlSlug = SeoUrl.FriendlyUrl(tagViewModel.Name);
                Tag tag = mapper.Map<Tag>(tagViewModel);

                unitOfWork.TagRepository.Create(tag);
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
                return NotFound();

            Tag? tag = unitOfWork.TagRepository.GetEntityById(id);
            if (tag is null)
                return NotFound();

            TagViewModel tagViewModel = mapper.Map<TagViewModel>(tag);

            return View(tagViewModel);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag tag = mapper.Map<Tag>(tagViewModel);

                unitOfWork.TagRepository.Update(tag);
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
                return NotFound();

            Tag? tag = unitOfWork.TagRepository.GetEntityById(id);

            if (tag == null)
                return NotFound();

            TagViewModel tagViewModel = mapper.Map<TagViewModel>(tag);

            return View(tagViewModel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult DeleteConfirmed(int id)
        {
            Tag? tag = unitOfWork.TagRepository.GetEntityById(id);
            if (tag == null)
            {
                TempData["error"] = "Delete fail!";
                return NotFound();
            }

            unitOfWork.TagRepository.Delete(tag);
            unitOfWork.SaveChanges();

            TempData["success"] = "Delete Successfully!";
            return RedirectToAction("Index", "Post");
        }
    }
}
