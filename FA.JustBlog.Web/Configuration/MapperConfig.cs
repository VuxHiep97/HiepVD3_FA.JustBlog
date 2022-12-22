using AutoMapper;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Web.Models;

namespace FA.JustBlog.Web.Configuration;

public class MapperConfig:Profile
{
	public MapperConfig()
	{
		CreateMap<Post, PostViewModel>().ReverseMap();
	}
}
