using LaptopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected IUowData Data;

        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        public BaseController()
            : this(new UowData())
        {
        }
    }
}