using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FA.JustBlog.ViewModels;

public class PostDetailViewModel
{
    [ValidateNever]
    public Post Post { get; set; } = null!;


    [ValidateNever]
    public IEnumerable<Tag> TagList { get; set; } = null!;

    [ValidateNever]
    public Category Category { get; set; } = null!;

    [ValidateNever]
    public IEnumerable<Comment> CommentList { get; set; } = null!;
}