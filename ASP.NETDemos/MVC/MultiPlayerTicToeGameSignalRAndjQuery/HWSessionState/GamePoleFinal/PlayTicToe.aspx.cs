using HWSessionState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HWSessionState.GamePoleFinal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string v = Request.QueryString["id"];
            string text= Request.QueryString["Text"];
            if (v == null)
            {
                Response.Redirect("~/Default");
                return;
            }
            UserManager manager = new UserManager();
            var user = manager.FindByNameAsync(HttpContext.Current.User.Identity.Name);
            var actuser = user.Result;
            manager.Dispose();
           
            Guid toComp=new Guid(v);
            using(ApplicationDbContext con=new ApplicationDbContext())
            {
              
                 var userbase= con.Users.Find(actuser.Id);
               
               Game elmt= con.Games.Find(toComp);
               if (text == "O" && elmt.Partcipiants.Count != 1)
               {
                   Response.Redirect("~/Default");
                   return;
               }
               else if(text=="X"&&elmt.Partcipiants.Count==1)
                 {
                     elmt.Partcipiants.Add(userbase);
                     elmt.Status = Status.Closed;
                     con.SaveChanges();
                 }
               else if (elmt == null || !elmt.Partcipiants.Contains(userbase))
               {
                   Response.Redirect("~/Default");
                   return;
               }                              

            }
        }
    }
}