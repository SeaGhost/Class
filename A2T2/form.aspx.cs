using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class form : System.Web.UI.Page
{
    BookReviewDbEntities1 db = new BookReviewDbEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var categories = from c in db.Categories
                             orderby c.CategoryName
                             select new { c.CategoryName, c.CategoryKey };
            DropDownList1.DataSource = categories.ToList();
            DropDownList1.DataTextField = "CategoryName";
            DropDownList1.DataValueField = "CategoryKey";
            DropDownList1.DataBind();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int key = int.Parse(DropDownList1.SelectedValue.ToString());
        var books = from b in db.Books
                    from a in b.Authors
                    from c in b.Categories
                    where c.CategoryKey == key
                    select new { b.BookTitle, a.AuthorName, b.BookISBN, c.CategoryName };
        GridView1.DataSource = books.ToList();
        GridView1.DataBind();

                    
    }
    
}