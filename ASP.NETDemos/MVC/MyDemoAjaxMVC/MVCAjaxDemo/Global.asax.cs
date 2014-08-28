using MVCAjaxDemo.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MVCAjaxDemo.Data.Migrations;


namespace MVCAjaxDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //comment after first intialization
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, MVCAjaxDemo.Data.Migrations.Configuration>());
            
        }
    }
}
