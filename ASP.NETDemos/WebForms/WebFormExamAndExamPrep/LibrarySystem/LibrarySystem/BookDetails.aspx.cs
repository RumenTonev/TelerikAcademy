using LibrarySystem.Models;
using RepeaterDemo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem
{
    public partial class BookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest request = this.Context.Request;
            string id = request.QueryString["id"];
            int idActual = Int32.Parse(id);
            using (var context = new LibrarySystemEntities())
            {
                var book = context.Books.FirstOrDefault(x => x.BookId == idActual);
                this.FormViewBook.DataSource = ObjectExtensions.WrapInEnumerable<Book>(book);
                this.FormViewBook.DataBind();
            }
        }
    }
}