using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExample.Web.Features.Base;

public class BaseDataModel
{
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [Column("created_user")]
    public int CreatedUser { get; set; }
    
    [Column("modified_date")]
    public DateTime? ModifiedDate { get; set; }
   
    [Column("modified_user")]
    public int ModifiedUser { get; set; }
    
    [Column("is_delete")]
    public bool IsDelete { get; set; }
}