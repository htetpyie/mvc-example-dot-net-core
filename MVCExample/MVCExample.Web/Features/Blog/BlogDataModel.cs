using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCExample.Web.Features.Base;

namespace MVCExample.Web.Features.Blog;

[Table("Tbl_Blog")]
public class BlogDataModel : BaseDataModel
{
    [Key]
    [Column("blog_id")]
    public int BlogId { get; set; }
    
    [Column("blog_title")]
    public string BlogTitle { get; set; }
    
    [Column("blog_author")]
    public string BlogAuthor { get; set; }
    
    [Column("blog_content")]
    public string BlogContent { get; set; }
}