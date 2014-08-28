using Error_Handler_Control;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

using System.Web.ModelBinding;


using LibrarySystem.Helpers;
using System.Data.Entity.Validation;


namespace LibrarySystem.Admin
{
    public partial class EditBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        // taka modelirano stava lesno za sortirane po navigation property!!!!!
        public IQueryable<BookModel> GridViewBooks_GetData()
        {
            
            try
            {
                LibrarySystemEntities db = new LibrarySystemEntities();
                var books = (from b in db.Books.ToList()
                             select new BookModel()
                             {
                                 Id = b.BookId,
                                 Title = b.Title.Substring(0, Math.Min(b.Title.Length, 20)) + "...",
                                 Author = b.Author,
                                 Description = b.Description,
                                 Isbn = b.ISBN,
                                 WebSite = b.WebSiteURL,
                                 Category = db.Categories.Find(b.CategoryId).Name
                             });
                return books.AsQueryable<BookModel>();
            }
            catch (Exception ex)
            {
                ErrorSuccessNotifier.AddErrorMessage(ex.Message);
            }

            return null;

           
        }

        protected void Delete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(e.CommandArgument);

                LibrarySystemEntities db = new LibrarySystemEntities();
                var currBook = db.Books.FirstOrDefault(c => c.BookId == id);

                if (currBook != null)
                {
                    db.Books.Remove(currBook);
                    db.SaveChanges();
                   
                    this.GridViewBooks.PageIndex = 0;
                    ErrorSuccessNotifier.AddInfoMessage("Book successfully deleted.");
                         this.GridViewBooks.DataBind();
                   // Response.Redirect("~/Admin/EditBooks.aspx",false);
                    
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                ErrorSuccessNotifier.AddErrorMessage(exceptionMessage + " " + ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                ErrorSuccessNotifier.AddErrorMessage(ex.Message);
            }
        }

        
        //public void GridViewBooks_DeleteItem(int bookId)
        //{
        //    LibrarySystemEntities context = new LibrarySystemEntities();
        //    Book book= context.Books.
        //        FirstOrDefault(q => q.BookId == bookId);
        //    try
        //    {
        //        context.Books.Remove(book);
        //        context.SaveChanges();
        //        this.GridViewBooks.PageIndex = 0;
        //        ErrorSuccessNotifier.AddInfoMessage("Question successfully deleted.");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorSuccessNotifier.AddErrorMessage(ex);
        //    }
        //}

        

        protected void GridViewBooks_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            this.GridViewBooks.PageIndex = e.NewPageIndex;
            
            }

        protected void Unnamed_Command(object sender, CommandEventArgs e)
        {

        }

        protected void Unnamed_Command1(object sender, CommandEventArgs e)
        {

        }
        //}
    }
}