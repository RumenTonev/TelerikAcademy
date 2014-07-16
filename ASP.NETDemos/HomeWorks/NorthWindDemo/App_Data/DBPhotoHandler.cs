using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace NorthWindDemo
{
    public class DBPhotoHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.RawUrl.Contains(".img2"))
            {

                int currentId = Int32.Parse(context.Request.QueryString["id"]);
                var dbContext = new NorthwindEntities();

                HttpResponse response = context.Response;
                var photo = dbContext.Employees.FirstOrDefault(x => x.EmployeeID == currentId).Photo;
                byte[] byteArray = photo;
                if (!(byteArray == null))
                {
                    ImageConverter ic = new ImageConverter();
                    Image img = (Image)ic.ConvertFrom(byteArray);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.WriteTo(response.OutputStream);
                    }
                    response.ContentType = "image/jpeg";
                }
                else
                {
                    //may not found recourse according to local IIS settings,may use ~ or ...
                    //or @"C:\directory\word.txt"; if its otn harddrive for example
                    Image img = Image.FromFile("/Images/pic.jpg");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.WriteTo(response.OutputStream);
                    }
                    response.ContentType = "image/jpeg";
                }
            }

        }
        public bool IsReusable
        {
            // Return true to keep the handler in memory (pooling).
            get { return false; }
        }
    }
}