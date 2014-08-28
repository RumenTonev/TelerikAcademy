using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LaptopSystem.Web.Models;
using LaptopSystem.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using LaptopSystem.Data;

namespace LaptopSystem.Web.Controllers
{
    public class LaptopsController : BaseController
    {
        const int PageSize = 5;

         public  LaptopsController(IUowData data):base(data)
            {
               // this.Data = data;
            }
        private IQueryable<LaptopViewModel> GetAllLaptops()
        {
            var data = this.Data.Laptops.All().Select(x => new LaptopViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Manufacturer = x.Manufacturer.Name,
                Model = x.Model,
                Price = x.Price,
            }).OrderBy(x => x.Id);

            return data;
        }

        public ActionResult KendoList()
        {
            return View();
        }

        //tova e za Kendo list alternativa na dolnoto bez Kendo
        //ToDataSourceResult iska Kendo.MVC.Extensions [DataSourceRequest] e ot Kendo
        public JsonResult GetLaptops([DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.GetAllLaptops().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult List(int? id)
        {
            //ako ne e podadeno id vzima 1
            int pageNumber = id.GetValueOrDefault(1);
            //paging
            var viewModel = GetAllLaptops().Skip((pageNumber - 1) * PageSize).Take(PageSize);
            //pazim kolko stranici ima vyv Viewbag
            ViewBag.Pages = Math.Ceiling((double)GetAllLaptops().Count() / PageSize);

            return View(viewModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                //extension methodi trqbva da sme referirali Identity
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                this.Data.Comments.Add(new Comment()
                {
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    LaptopId = commentModel.LaptopId,
                });

                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = username, Content = commentModel.Comment };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
        public JsonResult GetLaptopModelData(string text)
        {
            var result = this.Data.Laptops
                .All()
                .Where(x => x.Model.ToLower().Contains(text.ToLower()))
                .Select(x => new
                {
                    Model = x.Model
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(SubmitSearchModel submitModel)
        {
            var result = this.Data.Laptops.All();

            if (!string.IsNullOrEmpty(submitModel.ModelSearch))
            {
                result = result.Where(x => x.Model.ToLower().Contains(submitModel.ModelSearch.ToLower()));
            }

            if (submitModel.ManufSearch != "All")
            {
                result = result.Where(x => x.Manufacturer.Name == submitModel.ManufSearch);
            }

            if (submitModel.PriceSearch != 0)
            {
                result = result.Where(x => x.Price < submitModel.PriceSearch);
            }

            var endResult = result.Select(x => new LaptopViewModel
            {
                Id = x.Id,
                Model = x.Model,
                Manufacturer = x.Manufacturer.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            });

            return View(endResult);
        }
        public JsonResult GetLaptopManufacturerData()
        {
            var result = this.Data.Manufacturers
                .All()
                .Select(x => new
                {
                    ManufacturerName = x.Name
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Laptops
        public ActionResult Details(int id)
        {
            //tova e za MVC
            var userId = User.Identity.GetUserId();

            var viewModel = this.Data.Laptops.All().Where(x => x.Id == id)
                .Select(x => new LaptopDetailsViewModel
                {
                    Id = x.Id,
                    AdditionalParts = x.AdditionalParts,
                    Comments = x.Comments.Select(y => new CommentViewModel { AuthorUsername = y.Author.UserName, Content = y.Content }),
                    Description = x.Description,
                    HardDiskSize = x.HardDiskSize,
                    ImageUrl = x.ImageUrl,
                    ManufacturerName = x.Manufacturer.Name,
                    Model = x.Model,
                    MonitorSize = x.MonitorSize,
                    Price = x.Price,
                    RamMemorySize = x.RamMemorySize,
                    Weight = x.Weight,
                    UserCanVote = !x.Votes.Any(pesho => pesho.VotedById == userId),
                    Votes = x.Votes.Count()
                }).FirstOrDefault();

            return View(viewModel);
        }

        public ActionResult Vote(int id)
        {
            var userId = User.Identity.GetUserId();

            var canVote = !this.Data.Votes.All().Any(x => x.LaptopId == id && x.VotedById == userId);

            if (canVote)
            {
                this.Data.Laptops.GetById(id).Votes.Add(new Vote
                {
                    LaptopId = id,
                    VotedById = userId
                });

                this.Data.SaveChanges();
            }

            var votes = this.Data.Laptops.GetById(id).Votes.Count();

            return Content(votes.ToString());
        }
    }
}