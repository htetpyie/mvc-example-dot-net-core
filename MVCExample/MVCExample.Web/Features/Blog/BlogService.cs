using Microsoft.EntityFrameworkCore;
using MVCExample.Web.EFDbContext;
using MVCExample.Web.Features.DataTable;

namespace MVCExample.Web.Features.Blog;

public class BlogService : IBlogService
{
    private readonly AppDbContext _context;

    public BlogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BlogDataModel> GetById(int id)
    {
        var model = await _context
            .Blog
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.IsDelete == false && x.BlogId == id);

        return model;
    }

    public async Task<List<BlogDataModel>> GetList()
    {
        var blogList = await _context
            .Blog
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .ToListAsync();
        return blogList;
    }

    public async Task<DataTableResponse<BlogResponseModel>> GetList(
        BlogListRequestModel request)
    {
        DataTableResponse<BlogResponseModel> response = new DataTableResponse<BlogResponseModel>();

        try
        {
            var blogQuery = from blog in _context
                    .Blog
                    .AsNoTracking()
                where blog.IsDelete == false
                select new BlogResponseModel()
                {
                    BlogId = blog.BlogId,
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent
                };

            //Searching
            string searchValue = request?.DataTableRequest?.SearchValue.Trim();
            blogQuery = SearchQuery(blogQuery, searchValue);

            int recordTotal = blogQuery.Count();

            int skip = request.DataTableRequest.Skip;
            int pageSize = request.DataTableRequest.PageSize;
            var data = await blogQuery
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            response.Draw = request.DataTableRequest.Draw;
            response.RecordsFiltered = recordTotal;
            response.RecordsTotal = recordTotal;
            response.Data = data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return response;
    }

    public async Task<int> CreateBlog(BlogDataModel model)
    {
        int result = 0;
        try
        {
            model.CreatedDate = DateTime.Now; //Need Created User
            await _context.Blog.AddAsync(model);

            result = await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //Use Logger in Enterprise
            throw;
        }

        return result;
    }

    public async Task<int> UpdateBlog(int id, BlogDataModel model)
    {
        int result = 0;

        try
        {
            var blog = await _context
                .Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.IsDelete == false &&
                    x.BlogId == id);

            if (blog == null) return -1;

            blog.BlogTitle = model.BlogTitle;
            blog.BlogAuthor = model.BlogAuthor;
            blog.BlogContent = model.BlogContent;
            blog.ModifiedDate = DateTime.Now; //Need Modified User

            _context.Blog.Update(blog);
            _context.Entry(blog).State = EntityState.Modified;
            result = await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return result;
    }

    public async Task<int> DeleteBlog(int id)
    {
        int result = 0;
        try
        {
            var blog = await _context
                .Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.IsDelete == false && x.BlogId == id);
            if (blog == null) return -1;

            blog.IsDelete = true;
            blog.ModifiedDate = DateTime.Now; //Need deleted user
            _context.Entry(blog).State = EntityState.Modified;

            result = await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //add logger
            throw;
        }

        return result;
    }

    private IQueryable<BlogResponseModel> SearchQuery(
        IQueryable<BlogResponseModel> query, string searchValue)
    {
        if (!string.IsNullOrEmpty(searchValue))
        {
            query = query.Where(x => x.BlogTitle.Contains(searchValue)
                                     || x.BlogAuthor.Contains(searchValue)
                                     || x.BlogContent.Contains(searchValue));
        }

        return query;
    }

    public async Task<bool> IsDuplicate(BlogDataModel model)
    {
        bool isDuplicate = false;
        try
        {
            if (model.BlogId > 0) //for update
            {
                isDuplicate = await _context
                    .Blog
                    .AsNoTracking()
                    .AnyAsync(x =>
                        x.IsDelete == false &&
                        x.BlogId != model.BlogId &&
                        x.BlogTitle == model.BlogTitle &&
                        x.BlogAuthor == model.BlogAuthor);
            }
            else //for create
            {
                isDuplicate = await _context
                    .Blog
                    .AsNoTracking()
                    .AnyAsync(x =>
                        x.IsDelete == false &&
                        x.BlogTitle == model.BlogTitle &&
                        x.BlogAuthor == model.BlogAuthor);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return isDuplicate;
    }
}