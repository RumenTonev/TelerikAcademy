using Error_Handler_Control;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem.Admin
{
    public partial class EditCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<Category> GridViewCategories_GetData()
        {
            LibrarySystemEntities context = new LibrarySystemEntities();
            return context.Categories.OrderBy(q => q.CategoryId);
        }
        public void GridViewCategories_DeleteItem(int categoryId)
        {
            LibrarySystemEntities context = new LibrarySystemEntities();
            Category category = context.Categories.
                FirstOrDefault(q => q.CategoryId == categoryId);
            try
            {            
                context.Categories.Remove(category);
                context.SaveChanges();
                this.GridViewCategories.PageIndex = 0;
                ErrorSuccessNotifier.AddInfoMessage("Category successfully deleted.");
            }
            catch (Exception ex)
            {
                ErrorSuccessNotifier.AddErrorMessage(ex);
            }
        }
    }
}