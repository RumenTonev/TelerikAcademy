using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp
{
    public partial class _Default : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
           // {
              //  this.Redirect();
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                this.RepeaterMessages.DataSource = context.Messages.ToList();
                this.RepeaterMessages.DataBind();
            }
        }

        protected void ButtonSendMessage_Click(object sender, EventArgs e)
        {
            string contents = this.TextBoxMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(contents))
            {
                this.LabelErrorMessage.Text = "No message to send.";
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Find(User.Identity.GetUserId());
                if (user == null)
                {
                    this.LabelErrorMessage.Text = "User not logged in.";
                    return;
                }

                var newMessage = new Message
                {
                    Author = user,
                    Contents = contents,
                    Timestamp = DateTime.Now
                };

                context.Messages.Add(newMessage);
                context.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }
        }

        private void Redirect()
        {
            if(Request.QueryString["chat"]!=null)
            {
                Response.Redirect("~/Default.aspx", false);
            }
            else if (User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Administrator/AdministratorDefault.aspx", false);
            }
            else if (User.IsInRole("Moderator"))
            {
                Response.Redirect("~/Moderator/ModeratorDefault.aspx", false);
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.RepeaterMessages.DataBind();
        }

        protected void ButtonModertor_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Moderator/ModeratorDefault.aspx", false);
        }

        protected void ButtonAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administrator/AdministratorDefault.aspx", false);
        }
    }
}
