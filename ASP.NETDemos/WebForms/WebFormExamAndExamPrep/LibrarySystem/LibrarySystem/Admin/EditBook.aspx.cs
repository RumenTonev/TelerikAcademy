using Error_Handler_Control;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem.Admin
{
    public partial class EditBook : System.Web.UI.Page
    {
        bool isNewBook = false;
        private int bookId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.bookId =
                    Convert.ToInt32(Request.Params["bookId"]);
                isNewBook = (this.bookId == 0);
                if (isNewBook)
                {
                    this.ListBoxCategories.Visible = true;
                    LibrarySystemEntities context = new LibrarySystemEntities();
                    this.ListBoxCategories.DataSource = context.Categories.Select(x => x.Name).ToList();
                    this.ListBoxCategories.DataBind();
                }
                ViewState["newBook"] = isNewBook;
                ViewState["currentId"] = this.bookId;
            }

            if (!Convert.ToBoolean(ViewState["newBook"]))
            {

                using (LibrarySystemEntities context = new LibrarySystemEntities())
                {
                    int bookId = Int32.Parse(ViewState["currentId"].ToString());

                    Book book = context.Books.Find(bookId);
                    this.TextBoxTitle.Text = book.Title;
                    this.TextBoxAuthor.Text = book.Author;
                    this.TextBoxISBN.Text = book.ISBN;
                    this.TextBoxWebSiteURL.Text = book.WebSiteURL;
                    this.TextBoxDescription.Text = book.Description;
                }
            }        
        }

        protected void LinkButtonSaveCategory_Click(object sender, EventArgs e)
        {
            try{
            LibrarySystemEntities context = new LibrarySystemEntities();
            if (Convert.ToBoolean(ViewState["newBook"]))
            {
               var name= this.ListBoxCategories.SelectedValue;
                var curObj=context.Categories.FirstOrDefault(x=>x.Name==name);
                Book book = new Book() 
                {
                    Title = this.TextBoxTitle.Text,
                    Author = this.TextBoxAuthor.Text,
                    ISBN = this.TextBoxISBN.Text,
                     WebSiteURL = this.TextBoxWebSiteURL.Text,
                    Description = this.TextBoxDescription.Text,
                     CategoryId=curObj.CategoryId

                };
                context.Books.Add(book);
                context.SaveChanges();
                ErrorSuccessNotifier.AddInfoMessage("Book successfully created.");
            }
             
            else
            {

                int bookId = Int32.Parse(ViewState["currentId"].ToString());
                Book book = context.Books.Find(bookId);
                 book.Title=this.TextBoxTitle.Text;
                 book.Author=this.TextBoxAuthor.Text;
                 book.ISBN=this.TextBoxISBN.Text;
                book.WebSiteURL= this.TextBoxWebSiteURL.Text;
                book.Description = this.TextBoxDescription.Text;
                context.SaveChanges();
                ErrorSuccessNotifier.AddInfoMessage("Book successfully updated.");
            }
            
            Response.Redirect("~/Admin/EditBooks.aspx", false);
            ErrorSuccessNotifier.AddInfoMessage("Book successfully created.");

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
    }
}

