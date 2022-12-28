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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
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
                return NotFound();

            Category? category = unitOfWork.CategoryRepository.GetEntityById(id);

            if (category == null)
                return NotFound();

            CategoryViewModel categoryViewModel = mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }


        //Get
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                categoryViewModel.UrlSlug = SeoUrl.FriendlyUrl(categoryViewModel.Name);
                Category category = mapper.Map<Category>(categoryViewModel);

                unitOfWork.CategoryRepository.Create(category);
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

            Category? category = unitOfWork.CategoryRepository.GetEntityById(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel categoryViewModel = mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Update(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = mapper.Map<Category>(categoryViewModel);

                unitOfWork.CategoryRepository.Update(category);
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

            Category? category = unitOfWork.CategoryRepository.GetEntityById(id);

            if (category is null)
            {
                return NotFound();
            }

            CategoryViewModel categoryViewModel = mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult DeleteConfirmed(int id)
        {
            Category? category = unitOfWork.CategoryRepository.GetEntityById(id);

            if (category is null)
            {
                TempData["error"] = "Delete fail!";
                return NotFound();
            }

            unitOfWork.CategoryRepository.Delete(category);
            unitOfWork.SaveChanges();

            TempData["success"] = "Delete Successfully!";

            return RedirectToAction("Index", "Post");
        }
    }
}
