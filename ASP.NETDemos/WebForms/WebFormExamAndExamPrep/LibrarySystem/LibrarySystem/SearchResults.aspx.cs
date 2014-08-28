using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem
{
    public partial class SearchResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest request = this.Context.Request;
            string text = request.QueryString["text"];
           
            using (var context = new LibrarySystemEntities())
            {
                var books = context.Books.Where(x => x.Author.Contains(text) || x.Title.Contains(text)).OrderBy(x => x.Title.ToLower());




                this.BooksRepeater.DataSource = books.ToList();

                this.BooksRepeater.DataBind();
            }
        }
    }
}