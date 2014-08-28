using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterLikeApp.Data;
using TwitterLikeApp.Models;

namespace TwitterLikeApp.Controllers
{
    public class TagsController : Controller
    {
        // GET: Tags
        
        IUowData db;

        public TagsController(IUowData db)
        {
            this.db = db;
        }

        public TagsController()
        {
            db = new UowData();
        }

      
        public ActionResult Index(string tagName)
        {
            string fff = "#" + tagName;           // int id=db.Users.All().FirstOrDefault(x=>x.UserName==User.Identity.Name).Id
            Tag tag = db.Tags.All().FirstOrDefault(x => x.Name == fff);
           
            return View(tag.Messages);
          

        }

    }
}