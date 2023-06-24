using MVCExample.Web.Features.Blog;

namespace MVCExample.Web.Features;

public static class ChangeModel
{

    public static BlogDataModel Change(this BlogRequestModel model)
    {
        if (model == null) return new BlogDataModel();
        
        var data = new BlogDataModel()
        {
            Blog_Title = model.Blog_Title,
            Blog_Author = model.Blog_Author,
            Blog_Content = model.Blog_Content
        };

        return data;
    }
    
    public static BlogRequestModel Change(this BlogDataModel data)
    {
        if (data == null) return new BlogRequestModel();
        
        var model = new BlogRequestModel()
        {
            Blog_Id = data.Blog_Id,
            Blog_Title = data.Blog_Title,
            Blog_Author = data.Blog_Author,
            Blog_Content = data.Blog_Content
        };

        return model;
    }
    
}