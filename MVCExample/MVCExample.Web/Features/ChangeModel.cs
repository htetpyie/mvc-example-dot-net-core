using MVCExample.Web.Features.Blog;

namespace MVCExample.Web.Features;

public static class ChangeModel
{

    public static BlogDataModel Change(this BlogRequestModel model)
    {
        if (model == null) return new BlogDataModel();
        
        var data = new BlogDataModel()
        {
            BlogId = model.BlogId,
            BlogTitle = model.BlogTitle,
            BlogAuthor = model.BlogAuthor,
            BlogContent = model.BlogContent
        };

        return data;
    }
    
    public static BlogRequestModel Change(this BlogDataModel data)
    {
        if (data == null) return new BlogRequestModel();
        
        var model = new BlogRequestModel()
        {
            BlogId = data.BlogId,
            BlogTitle = data.BlogTitle,
            BlogAuthor = data.BlogAuthor,
            BlogContent = data.BlogContent
        };

        return model;
    }
    
}