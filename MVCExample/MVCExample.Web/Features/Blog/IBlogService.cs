using MVCExample.Web.Features.DataTable;

namespace MVCExample.Web.Features.Blog;

public interface IBlogService
{
    Task<BlogDataModel> GetById(int id);
    Task<List<BlogDataModel>> GetList();
    Task<DataTableResponse<BlogResponseModel>> GetList(BlogListRequestModel model);
    Task<int> CreateBlog(BlogDataModel model);
    Task<int> UpdateBlog(int id, BlogDataModel model);
    Task<int> DeleteBlog(int id);
    Task<bool> IsDuplicate(BlogDataModel model);
}