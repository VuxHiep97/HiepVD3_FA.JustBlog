using AutoMapper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Posts
        public IActionResult Index()
        {
            List<PostViewModel> postViewModel = mapper.Map<List<PostViewModel>>(unitOfWork.PostRepository.GetEntities().ToList());

            return View(postViewModel);
        }

        // GET: Posts/Latest
        public IActionResult Latest()
        {
            List<PostViewModel> postViewModel = mapper.Map<List<PostViewModel>>(unitOfWork.PostRepository.GetEntities().OrderByDescending(p => p.PostedOn).ToList());

            return View("Index", postViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Post? post = unitOfWork.PostRepository.GetEntityById(id.Value);

                if (post is null)
                    return RedirectToAction("Index");

                PostViewModel postViewModel = mapper.Map<PostViewModel>(post);

                return View(postViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}
