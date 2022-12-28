using AutoMapper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;
using FA.JustBlog.Utility;
using FA.JustBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class PostController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public PostController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public IActionResult Index()
		{
			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				(unitOfWork.PostRepository.GetAllPosts());

			return View(postViewModels);
		}

		public IActionResult LatestPosts()
		{
			ViewBag.Title = "Latest posts!";

			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				(unitOfWork.PostRepository.GetLatestPost(5));

			return View("PostDisplay", postViewModels);
		}

		public IActionResult MostViewedPosts()
		{
			ViewBag.Title = "Most viewed!";

			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				(unitOfWork.PostRepository.GetMostViewedPost(5));

			return View("PostDisplay", postViewModels);
		}

		public IActionResult PostsByCategory(string categoryName)
		{
			ViewBag.Title = "Post by Category";

			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				(unitOfWork.PostRepository.GetPostsByCategory(categoryName));

			return View("PostDisplay", postViewModels);
		}

		public IActionResult PostsByTagName(string tagName)
		{
			ViewBag.Title = "Post by Tag";

			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				(unitOfWork.PostRepository.GetPostsByTag(tagName));

			return View("PostDisplay", postViewModels);
		}

		public IActionResult MostInterestingPosts()
		{
			ViewBag.Title = "Most interesting Posts!";

			IEnumerable<Post> objPostList = unitOfWork.PostRepository.GetMostViewedPost(5);
			return View(objPostList);
		}

		[Authorize(Roles = Roles.BLOG_OWNER)]
		public IActionResult PublishedPosts()
		{
			ViewBag.Title = "Published";

			IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
				 (unitOfWork.PostRepository.GetPublishedPosts());

			return View("PostDisplay", postViewModels);
		}

		//[Authorize(Roles = Roles.BLOG_OWNER)]
		//public IActionResult UnPublishedPosts()
		//{
		//	IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>
		//		(unitOfWork.PostRepository.GetUnpublishedPosts());

		//	return View("PostDisplay", postViewModels);
		//}
	}
}
