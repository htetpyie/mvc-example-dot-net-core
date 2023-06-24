using Microsoft.AspNetCore.Mvc;
using MVCExample.Web.Features.Base;
using MVCExample.Web.Features.DataTable;

namespace MVCExample.Web.Features.Blog;

public partial class BlogController : BaseController
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

    [HttpPost]
    public async Task<IActionResult> GetBlogList()
    {
        BlogListRequestModel request = new BlogListRequestModel();
        DataTableRequest requestFromDataTable = GetDataTableRequest();
        request.DataTableRequest = requestFromDataTable;

        var response = await _iBlogService.GetList(request);

        return Ok(response);
    }

    public IActionResult CreateBlog(BlogRequestModel model)
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

            if (await _iBlogService.IsDuplicate(data))
            {
                TempData["Message"] = "Error! Blog is already created !";
                return RedirectToAction(nameof(CreateBlog), model);
            }

            int result = await _iBlogService.CreateBlog(data);
            message = result > 0
                ? "Blog is created successfully !"
                : "Create Error";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            message = "Something went wrong ! Please try again later.";
        }

        TempData["Message"] = message;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditBlog(string id)
    {
        BlogRequestModel model = new BlogRequestModel();

        try
        {
            bool isInt = int.TryParse(id, out int blogId);

            if (!isInt)
            {
                TempData["Message"] = "404 Not Found.";
                return RedirectToAction(nameof(Index));
            }

            var data = await _iBlogService.GetById(blogId);
            model = data.Change();
        }
        catch (Exception e)
        {
            TempData["Message"] = "Something went wrong! Please try again later.";
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBlog(BlogRequestModel model)
    {
        string message = String.Empty;
        try
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(EditBlog), model.BlogId);
            }
            
            BlogDataModel data = model.Change();

            if (await _iBlogService.IsDuplicate(data))
            {
                TempData["Message"] = "Error! Blog is already created !";
                return View("EditBlog", model);
            }

            int result = await _iBlogService.UpdateBlog(model.BlogId, data);
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
            message = result > 0
                ? "Blog is deleted successfylly !"
                : "Blog deletion error !";
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