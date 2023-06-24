using Microsoft.AspNetCore.Mvc;

namespace MVCExample.Web.Features.Blog;

public class BlogController : Controller
{
    private readonly IBlogService _iBlogService;
    
    public BlogController(IBlogService iBlogService)
    {
        _iBlogService = iBlogService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetBlogList()
    {
        return Json("");
    }

    public IActionResult CreateBlog( BlogRequestModel model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveBlog(BlogRequestModel model)
    {
        string message = String.Empty;
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(CreateBlog), model);
            }

            BlogDataModel data = model.Change();

            int result = await _iBlogService.CreateBlog(data);
            message = result > 0
                ? "Blog is created successfully!"
                : "Create Error";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            message = "Something went wrong! Please try again later.";
        }

        TempData["Message"] = message;
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> EditBlog(string id)
    {
        string message = string.Empty;
        try
        {
            bool isInt = int.TryParse(id, out int blogId);
            
            if (!isInt)
            {
                TempData["Message"] = "404 Not Found.";
                return RedirectToAction(nameof(Index));
            }

            var data = await _iBlogService.GetById(blogId);
            BlogRequestModel model = data.Change();

        }
        catch (Exception e)
        {
            message = "Something went wrong! Please try again later.";
        }

        TempData["Message"] = message;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBlog(BlogRequestModel model)
    {
        string message = String.Empty;
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(EditBlog), model.Blog_Id);
            }

            BlogDataModel data = model.Change();

            int result = await _iBlogService.UpdateBlog(model.Blog_Id, data);
            message = result > 0
                ? "Blog is updated successfully!"
                : "Update Error";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            message = "Something went wrong! Please try again later.";
        }

        TempData["Message"] = message;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBlog(string id)
    {
        string message = String.Empty;
        try
        {
            bool isInt = int.TryParse(id, out int blogId);
            if (!isInt)
            {
                TempData["Message"] = "404 Not Found.";
                return RedirectToAction(nameof(Index));
            }

            int result = await _iBlogService.DeleteBlog(blogId);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            message = "Something went wrong! Please try again later.";
        }

        TempData["Message"] = message;
        return RedirectToAction(nameof(Index));
    }
}