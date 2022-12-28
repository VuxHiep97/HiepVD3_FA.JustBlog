using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FA.JustBlog.ViewModels;

public class PostViewModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Post title is required")]
    public string Title { get; set; } = null!;

    [Column("Short Description")]
    [StringLength(255)]
    public string ShortDescription { get; set; } = null!;

    [Column("Post Content")]
    [Required(ErrorMessage = "Post content is required")]
    public string PostContent { get; set; } = null!;

    [StringLength(255)]
    public string UrlSlug { get; set; } = null!;

    [Required]
    public bool Published { get; set; }

    [Column("Posted On")]
    public DateTime PostedOn { get; set; }

    public DateTime Modified { get; set; }

    public int ViewCount { get; set; }

    [DefaultValue(0)]
    public int RateCount { get; set; }
    public int TotalRate { get; set; }

    private decimal rate;
    public decimal Rate
    {
        get => rate;
        set
        {
            if (RateCount > 0)
                rate = TotalRate / RateCount;
            else
                rate = 0;
        }
    }

    public int CategoryId { get; set; }
    //public Category Category { get; set; } = null!;

    [ValidateNever] 
    public Post Post { get; set; } = null!;

    [ValidateNever] 
    public IEnumerable<SelectListItem> AllCategoryList { get; set; } = null!;

    [ValidateNever] 
    public string TagsSelected { get; set; } = null!;
    
}
