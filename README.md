## MVC CRUD Example with Blog Table
* This is CRUD project example for new Juniors.

#### Used Technology
* ASP.NET Core 6.0 and Entity Framework Core with SQL Server,
* Admin Lte Template,
* JQuery Datatable,
* JQuery Validation,
* Bootstrap Modal Box

#### Guide
1. Clone the project
2. Change DbConnection value to your database connection (in appsetting.json)
3. Create Blog Table in your database (script is below)
4. Run Project

#### Blog Table
```
CREATE TABLE [dbo].[tbl_blog](
	[blog_id] [int] IDENTITY(1,1) NOT NULL,
	[blog_title] [nvarchar](50) NULL,
	[blog_author] [nvarchar](50) NULL,
	[blog_content] [nvarchar](200) NULL,
	[created_date][datetime] NULL,
	[created_user][int] Null,
	[modified_date][datetime] NULL,
	[modified_user][int] NULL,
	[is_delete][bit] Default 0
) ON [PRIMARY]

```

#### Db Connection String Example
```
"DbConnection": "Server=?;Database =?;User Id=?;Password=?;Trusted_Connection=True;TrustServerCertificate=true"
```

### Refrences
* [AdminLte Template](https://adminlte.io/themes/v3/)
* [JQuery DataTable In ASP Net Core](https://codewithmukesh.com/blog/jquery-datatable-in-aspnet-core/)
* [Model Validation](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0)

