using System.ComponentModel.DataAnnotations;

namespace MVCExample.Web.Features.Blog;

public class BlogRequestModel
{
    public int Blog_Id { get; set; }
    
    [Required]
    public string Blog_Title { get; set; }
    
    [Required]
    public string Blog_Author { get; set; }
    
    [Required]
    public string Blog_Content { get; set; }
}