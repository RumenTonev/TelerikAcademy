using HWSessionState.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HWSessionState
{
    public partial class _Default : Page
    {
        
      
        protected void Page_Load(object sender, EventArgs e)
        {
     
                using (var context = new ApplicationDbContext())
                {
                    this.RepeaterGamesArchived.DataSource = context.Games.Where(x => x.Status == Status.Archived).ToList();
                    this.RepeaterGamesArchived.DataBind();
                    this.RepeaterGamesOpen.DataSource = context.Games.Where(x => x.Status == Status.Open).ToList();
                    this.RepeaterGamesOpen.DataBind();
                    this.RepeaterGamesClosed.DataSource = context.Games.Where(x => x.Status == Status.Closed).ToList();
                    this.RepeaterGamesClosed.DataBind();
                }
            
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.RepeaterGamesArchived.DataBind();
            this.RepeaterGamesOpen.DataBind();
            this.RepeaterGamesClosed.DataBind();
        }

       
        protected void RepeaterGamesOpen_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType !=
                ListItemType.AlternatingItem)
            { return; }
             Game row = (Game)e.Item.DataItem;
            string currentId = row.Id.ToString();
            LinkButton btn = (LinkButton)e.Item.FindControl("HyperLink");
            btn.PostBackUrl = "~/GamePoleFinal/PlayTicToe.aspx?id=" + currentId + "&Text=X";
            btn.Text = "PlayGame:" + currentId;
                      
        }

        protected void LinkButtonNewGame_Click(object sender, EventArgs e)
        {
           string gameId=null;
            
            using (ApplicationDbContext con = new ApplicationDbContext())
            {

                var game = new Game
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Open,


                };
                
                
                con.Games.Add(game);
                con.SaveChanges();
                
               // var actuser = user.Result;
               // manager.Dispose();
               


               // if (actuser == null) { Response.Redirect("~/Default"); return; }
                
               // con.SaveChanges();gameId = game.Id.ToString();
               gameId = game.Id.ToString();
            }
            using (ApplicationDbContext con2 = new ApplicationDbContext())
            {
                Guid toComp = new Guid(gameId);
                var game = con2.Games.Find(toComp);
                UserManager manager = new UserManager();
                var user = manager.FindByNameAsync(HttpContext.Current.User.Identity.Name);
                var userAct = con2.Users.FirstOrDefault(x => x.Id == user.Result.Id);
                manager.Dispose();
                game.Partcipiants.Add(userAct);
               // game.Partcipiants.
                
                con2.SaveChanges();
                // var actuser = user.Result;
                // manager.Dispose();



                // if (actuser == null) { Response.Redirect("~/Default"); return; }

                // con.SaveChanges();

            }
          // string gameID = game.Id.ToString();
           Response.Redirect("~/GamePoleFinal/PlayTicToe" + "?id=" + gameId+"&Text=O");
        }

        protected void RepeaterGamesOpen_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           // var button = (sender as LinkButton);
            if (e.CommandName == "cmd")
            {
                LinkButton button = e.CommandSource as LinkButton;
                
            }
            UserManager manager = new UserManager();
            var user = manager.FindByNameAsync(HttpContext.Current.User.Identity.Name);

            manager.Dispose();
            //UserManager manager = new UserManager();
            // var user = manager.FindByNameAsync(HttpContext.Current.User.Identity.Name);
            //var userAct = con2.Users.FirstOrDefault(x => x.Id == user.Result.Id);
            // manager.Dispose();
            // game.Partcipiants.Add(userAct);

            Game row = (Game)e.Item.DataItem;
            string currentId = row.Id.ToString();

            using (var context = new ApplicationDbContext())
            {
                var userAct = context.Users.FirstOrDefault(x => x.Id == user.Result.Id);
                var currentGame = context.Games.FirstOrDefault(x => x.Id.ToString() == currentId);
                currentGame.Status = Status.Closed;
                currentGame.Partcipiants.Add(userAct);
                context.SaveChanges();

            }
        }
    
    }
}