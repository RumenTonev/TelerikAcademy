using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaptopSystem.Web.Controllers;
using System.Web.Mvc;
using LaptopSystem.Web.Models;
using System.Collections.Generic;
using LaptopSystem.Data;
using Moq;
using System.Web;
using LaptopSystem.Models;
using System.Linq;
using System.Web.Routing;
using System.Web.Caching;
using System.Collections;

namespace LaptopSystem.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
       
        [TestMethod]
        public void IndexMethodShouldReturnProperNumberOfBugs()
        {

            var list = new List<Laptop>();
            list.Add(new Laptop() {
             Id=1,
              Model="Lenovo",
             Manufacturer = new Manufacturer() { Name = "Lenovo3" },
                Price=3
            });
            list.Add(new Laptop()
            {
                Id = 2,
                Manufacturer=new Manufacturer(){Name="Lenovo3"},
                Model = "Lenovo",
                Price = 3
            });
            var bugsRepoMock = new Mock<IRepository<Laptop>>();
            bugsRepoMock.Setup(x => x.All()).Returns(list.AsQueryable());

            var uofMock = new Mock<IUowData>();
            uofMock.Setup(x => x.Laptops).Returns(bugsRepoMock.Object);
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("GET");
            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(request.Object);
            //mockHttpContext.Setup(m => m.Cache).Returns(cache.Object);
            var mockControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), new Mock<ControllerBase>().Object);

            var controller = new HomeController(uofMock.Object)
            {
                ControllerContext = mockControllerContext

            };



            //  var controller = new HomeController(uofMock.Object);



            var viewResult = controller.Index() as ViewResult;
            Assert.IsNotNull(viewResult, "Index action returns null.");
            var model = viewResult.Model as IEnumerable<LaptopViewModel>;
            Assert.IsNotNull(model, "The model is null.");
            Assert.AreEqual(2, model.Count());
        }
    }
}
