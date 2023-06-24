namespace MVCExample.Web.Features;

public class BaseDataModel
{
    public DateTime CreatedDate { get; set; }
    public int CreatedUser { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int ModifiedBy { get; set; }
    public bool IsDelete { get; set; }
}