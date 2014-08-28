using LaptopSystem.Data;
using LaptopSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;

namespace LaptopSystem.Web.Controllers
{
    public class HomeController : BaseController
    {

         public  HomeController(IUowData data):base(data)
            {
                //this.Data = data;
            }
        public ActionResult Index()
        {
            //WITH CACHING
            ObjectCache cache = MemoryCache.Default;
            if ( cache["HomePageLaptops"] == null)
            {
                var listOfLaptops = this.Data.Laptops.All()
                   .OrderByDescending(x => x.Votes.Count())
                    .Take(6)
                    .Select(x => new LaptopViewModel
                    {
                        Id = x.Id,
                        Manufacturer = x.Manufacturer.Name,
                       ImageUrl = x.ImageUrl,
                        Model = x.Model,
                        Price = x.Price,
                    });

                cache.Add("HomePageLaptops", listOfLaptops, DateTime.Now.AddHours(1));
                
            }

            return View(cache["HomePageLaptops"]);
            
        }

        
    }
}