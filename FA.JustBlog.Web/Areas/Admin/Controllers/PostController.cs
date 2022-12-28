using AutoMapper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;
using FA.JustBlog.Utility;
using FA.JustBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            IEnumerable<PostViewModel> postViewModels = mapper.Map<IEnumerable<PostViewModel>>(unitOfWork.PostRepository.GetEntities());

            return View(postViewModels);
        }

        public IActionResult Details(int id)
        {
            Post? post = unitOfWork.PostRepository.FindPost(id);

            if (post == null)
                return NotFound();
            
            PostDetailViewModel postDetailViewModel = new()
            {
                Post = post,
            };

            postDetailViewModel.Category = unitOfWork.CategoryRepository.GetEntityById(postDetailViewModel.Post.CategoryId);
            postDetailViewModel.TagList = unitOfWork.TagRepository.GetTagsByPostId(postDetailViewModel.Post.Id);
            postDetailViewModel.CommentList = unitOfWork.CommentRepository.GetCommentsForPost(postDetailViewModel.Post.Id);

            return View(postDetailViewModel);
        }

        public IActionResult DetailsPostWithTime(string title, int year, int month)
        {
            var post = unitOfWork.PostRepository.FindPost(title, year, month);

            if (post == null)
                return NotFound();

            PostDetailViewModel postDetailViewModel = new()
            {
                Post = post,
            };

            postDetailViewModel.Category = unitOfWork.CategoryRepository.GetEntityById(postDetailViewModel.Post.CategoryId);
            postDetailViewModel.TagList = unitOfWork.TagRepository.GetTagsByPostId(postDetailViewModel.Post.Id);
            postDetailViewModel.CommentList = unitOfWork.CommentRepository.GetCommentsForPost(postDetailViewModel.Post.Id);

            return View("Details", postDetailViewModel);
        }

        //Get
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]

        public IActionResult Create()
        {
            PostViewModel postViewModel = new()
            {
                AllCategoryList = unitOfWork.CategoryRepository.GetAllCategories().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
            };
            return View(postViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]

        public IActionResult Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var tagIds = unitOfWork.TagRepository.AddTagByString(postViewModel.TagsSelected);

                var postTags = new List<PostTagMap>();
                foreach (var tagId in tagIds)
                {
                    var postTag = new PostTagMap()
                    {
                        TagId = tagId
                    };
                    postTags.Add(postTag);
                }

                Post post = new()
                {
                    Title = postViewModel.Post.Title,
                    UrlSlug = SeoUrl.FriendlyUrl(postViewModel.Post.Title),
                    PostContent = postViewModel.Post.PostContent,
                    CategoryId = postViewModel.Post.CategoryId,
                    ShortDescription = postViewModel.Post.ShortDescription,
                    PostTags = postTags,
                };

                unitOfWork.PostRepository.Create(post);
                unitOfWork.SaveChanges();

                TempData["success"] = "Post created successfully";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Post created error";
            ViewBag.CategoryList = unitOfWork.CategoryRepository.GetAllCategories().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.PublishedList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "True", Value = true.ToString(), Selected = true },
                new SelectListItem() { Text = "False", Value = false.ToString(), Selected = false },
            };
            return View(postViewModel);
        }

        //Get
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Update(int? postId)
        {
            if (postId == null || postId == 0)
            {
                return NotFound();
            }

            PostViewModel postViewModel = new()
            {
                Post = unitOfWork.PostRepository.GetEntityById(postId),
                AllCategoryList = unitOfWork.CategoryRepository.GetAllCategories().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
            };

            StringBuilder selectedTag = new StringBuilder();
            if (postViewModel.Post?.PostTags != null)
            {
                foreach (var map in postViewModel.Post.PostTags)
                {
                    selectedTag.Append(map.Tag.Name + ";");
                }
            }
            postViewModel.TagsSelected = selectedTag.ToString();

            if (postViewModel.Post == null)
            {
                return NotFound();
            }
            return View(postViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.CONTRIBUTOR + "," + Roles.BLOG_OWNER)]
        public IActionResult Update(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var tagIds = unitOfWork.TagRepository.AddTagByString(postViewModel.TagsSelected);
                var postTags = new List<PostTagMap>();

                foreach (var tagId in tagIds)
                {
                    var postTag = new PostTagMap()
                    {
                        TagId = tagId,
                        PostId = postViewModel.Post.Id
                    };
                    postTags.Add(postTag);
                }

                Post post = new()
                {
                    Id = postViewModel.Post.Id,
                    Title = postViewModel.Post.Title,
                    UrlSlug = postViewModel.Post.UrlSlug,
                    PostContent = postViewModel.Post.PostContent,
                    CategoryId = postViewModel.Post.CategoryId,
                    ShortDescription = postViewModel.Post.ShortDescription,
                    PostTags = postTags,

                };

                if (post.UrlSlug == null)
                {
                    post.UrlSlug = SeoUrl.FriendlyUrl(post.Title);
                }

                unitOfWork.PostRepository.Update(post);
                unitOfWork.SaveChanges();

                TempData["success"] = "Post created successfully";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Post created error";
            ViewBag.CategoryList = unitOfWork.CategoryRepository.GetAllCategories().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.PublishedList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "True", Value = true.ToString(), Selected=true },
                new SelectListItem() { Text = "False", Value = false.ToString(), Selected=false },
            };

            return View(postViewModel);
        }

        //Get
        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult Delete(int? postId)
        {
            if (postId == null || postId == 0)
            {
                return NotFound();
            }

            PostViewModel postViewModel = new()
            {
                Post = unitOfWork.PostRepository.GetEntityById(postId),
                AllCategoryList = unitOfWork.CategoryRepository.GetAllCategories().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
            };

            if (postViewModel.Post == null)
            {
                return NotFound();
            }

            StringBuilder selectedTag = new StringBuilder();
            if (postViewModel.Post.PostTags != null)
            {
                foreach (var map in postViewModel.Post.PostTags)
                {
                    selectedTag.Append(map.Tag.Name + ";");
                }
            }

            postViewModel.TagsSelected = selectedTag.ToString();

            return View(postViewModel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = Roles.BLOG_OWNER)]
        public IActionResult DeleteConfirmed(int? postId)
        {
            Post post = unitOfWork.PostRepository.GetEntityById(postId);
            if (post == null)
            {
                TempData["error"] = "Delete fail!";
                return NotFound();
            }

            unitOfWork.PostRepository.Delete(post);
            unitOfWork.SaveChanges();

            TempData["success"] = "Delete Successfully!";

            return RedirectToAction("Index");
        }
    }
}
