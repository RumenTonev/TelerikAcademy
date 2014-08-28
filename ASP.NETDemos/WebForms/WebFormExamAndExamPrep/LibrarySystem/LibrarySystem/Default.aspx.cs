using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibrarySystem
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (LibrarySystemEntities context = new LibrarySystemEntities())
            {
                //randomizing taka vseki pyt gi sortira po-razli4en na4in
                var categories = context.Categories.Include("Books");
               
                
                    this.ListViewCategories.DataSource = categories.ToList();
                
                this.DataBind();
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            
                var text = this.TextBoxSearch.Text;
                //randomizing taka vseki pyt gi sortira po-razli4en na4in
                //var categories = context.Categories.Include("Books");
               // this.ListViewCategories.DataSource = categories.ToList();
                //this.DataBind();
                Response.Redirect("SearchResults.aspx?text=" + text);
            
        }

       
        //da pokazva nadpisa kogato nqma infromaciq za pokazavane
        protected void RepeaterBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var repeater=sender as Repeater;
            if (repeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }
    }
}