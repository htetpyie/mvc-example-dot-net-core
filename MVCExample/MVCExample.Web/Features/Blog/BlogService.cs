using Microsoft.EntityFrameworkCore;
using MVCExample.Web.EFDbContext;

namespace MVCExample.Web.Features.Blog;

public interface IBlogService
{
    Task<BlogDataModel> GetById(int id);
    Task<List<BlogDataModel>> GetList();
    Task<int> CreateBlog(BlogDataModel model);
    Task<int> UpdateBlog(int id, BlogDataModel model);
    Task<int> DeleteBlog(int id);
}

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
                x.IsDelete == false && x.Blog_Id == id);

        return model;
    }

    public async Task<List<BlogDataModel>> GetList()
    {
        var blogList = await _context
            .Blog
            .AsNoTracking()
            .ToListAsync();
        return blogList;
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
            Console.WriteLine(e); //User Logger in Enterprise
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
                    x.Blog_Id == id);

            if (blog == null) return -1;

            blog.Blog_Title = model.Blog_Title;
            blog.Blog_Author = model.Blog_Author;
            blog.Blog_Content = model.Blog_Content;
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
                    x.IsDelete == false && x.Blog_Id == id);
            if (blog == null) return -1;

            blog.IsDelete = true;
            blog.ModifiedDate = DateTime.Now;//Need deleted user
            _context.Entry(blog).State = EntityState.Deleted;

            result = await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //add logger
            throw;
        }
        return result;
    }
}