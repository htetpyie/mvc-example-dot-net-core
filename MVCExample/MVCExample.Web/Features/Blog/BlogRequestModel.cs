using System.ComponentModel.DataAnnotations;

namespace MVCExample.Web.Features.Blog;

public class BlogRequestModel
{
    public int BlogId { get; set; }

    [Required] [StringLength(50)] public string BlogTitle { get; set; }

    [Required] [StringLength(50)] public string BlogAuthor { get; set; }

    [Required][StringLength(200)] public string BlogContent { get; set; }
}