using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.ViewModels;

public class PostUpdateViewModel
{
    [ValidateNever] 
    public Post Post { get; set; } = null!;

    [ValidateNever] 
    public IEnumerable<SelectListItem> AllCategoryList { get; set; } = null!;
    
    [ValidateNever] 
    public Tag Tag { get; set; } = null!;

    [ValidateNever] 
    public string CategoryIdSelected { get; set; } = null!;

    public string TagsSelected { get; set; } = null!;
}
