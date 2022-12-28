using AutoMapper;
using FA.JustBlog.Models;
using FA.JustBlog.ViewModels;

namespace FA.JustBlog.Web.Configuration;

public class MapperConfig:Profile
{
	public MapperConfig()
	{
		CreateMap<Post, PostViewModel>().ReverseMap();
		CreateMap<Post, PostDetailViewModel>().ReverseMap();
		CreateMap<Post, PostUpdateViewModel>().ReverseMap();

		CreateMap<Comment, CommentViewModel>().ReverseMap();

		CreateMap<Tag, TagViewModel>().ReverseMap();
	}
}
