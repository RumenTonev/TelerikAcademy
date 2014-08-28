using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterLikeApp.Data;
using System.Data.Entity;
using TwitterLikeApp.Models;
using TwitterLikeApp.ViewModels;

using System.Text.RegularExpressions;
namespace TwitterLikeApp.Controllers { 


[Authorize]
    public class MessagesController : Controller
    {


        IUowData db;

        public MessagesController(IUowData db)
        {
            this.db = db;
        }

        public MessagesController()
        {
            db = new UowData();
        }

      
        public ActionResult Index()
        {
           
            var tweets = db.Messages.All().Include("Author").Where(x => x.Author.UserName == User.Identity.Name);

            return View(tweets);
          

        }

        public ActionResult Create()
        {
           // ViewBag.Categories = db.BugsCategories.ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            return View();
        }

        // POST: /BugsAdministration/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetViewModel model)
        {
            

            if (ModelState.IsValid)
            {
               
                Message mes = new Message()
                {
                    Author = db.Users.All().FirstOrDefault(x => x.UserName == User.Identity.Name),
                    Content =  Regex.Replace( model.Content, @"\ +", " ").Trim(),
                   
                    CreatedOn = DateTime.Now
                };
                db.Messages.Add(mes);
                db.SaveChanges();


                var words = mes.Content.Split();
                
                for (int i = 0; i < words.Length; i++)
                {

                    string currentVal = words[i];

                    if (currentVal.Length>0&&currentVal[0] == '#')
                    {
                        var newTag = new Tag()
                        {
                            Name = currentVal
                        };
                        var tag = db.Tags.All().FirstOrDefault(x => x.Name == currentVal);
                        if (tag != null)
                        {
                            mes.Tags.Add(tag);

                        }
                        else
                        {
                            db.Tags.Add(newTag);
                            mes.Tags.Add(newTag);
                        }
                        db.SaveChanges();

                       
                       
                    }
                }
                
                return RedirectToAction("Index");
            }


            return View(model);
        }
    }
}