using System.ComponentModel.DataAnnotations;

namespace MVCExample.Web.Features.Blog;

public class BlogDataModel : BaseDataModel
{
    [Key]
    public int Blog_Id { get; set; }
    public string Blog_Title { get; set; }
    public string Blog_Author { get; set; }
    public string Blog_Content { get; set; }
}