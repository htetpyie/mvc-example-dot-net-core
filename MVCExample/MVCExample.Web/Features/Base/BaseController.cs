using Microsoft.AspNetCore.Mvc;
using MVCExample.Web.Features.DataTable;

namespace MVCExample.Web.Features.Base;

public class BaseController : Controller
{
    protected DataTableRequest GetDataTableRequest()
    {
        DataTableRequest request = new DataTableRequest();
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            request = new DataTableRequest
            {
                Draw = draw,
                Start = start,
                Length = length,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue,
                PageSize = pageSize,
                Skip = skip
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return request;
    }
}