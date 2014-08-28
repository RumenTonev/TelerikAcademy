using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem.Admin
{
    public partial class EditCategory : System.Web.UI.Page
    {
        bool isNewCategory = false;
        private int categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.categoryId =
                    Convert.ToInt32(Request.Params["categoryId"]);
                isNewCategory = (this.categoryId == 0);
                ViewState["newCategory"] = isNewCategory;
                ViewState["currentId"] = this.categoryId;
            }

            if (!Convert.ToBoolean(ViewState["newCategory"]))
            {

                using (LibrarySystemEntities context = new LibrarySystemEntities())
                {
                    int categoryId = Int32.Parse(ViewState["currentId"].ToString());
                    Category category = context.Categories.Find(categoryId);
                    this.TextBoxCategoryName.Text = category.Name;

                }
            }

        }

        protected void LinkButtonSaveCategory_Click(object sender, EventArgs e)
        {
            LibrarySystemEntities context = new LibrarySystemEntities();
            if (Convert.ToBoolean(ViewState["newCategory"]))
            {

                Category category = new Category() { Name = this.TextBoxCategoryName.Text };
                context.Categories.Add(category);
                context.SaveChanges();
            }

            else
            {

                int categoryId = Int32.Parse(ViewState["currentId"].ToString());
                Category category = context.Categories.Find(categoryId);
                category.Name = this.TextBoxCategoryName.Text;
                context.SaveChanges();

            }
            Response.Redirect("~/Admin/EditCategories.aspx", false);
        }

    }
}