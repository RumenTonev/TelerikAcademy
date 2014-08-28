using System;
using System.IO;
using System.Text;
using System.Web.Caching;

namespace HWCache
{
    public partial class CacheDepend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //   var filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\file.txt";
            
            var folderPath = @"D:\TelerikAcademy\ASP.NETDemos\HomeWorks\HWCache\HWCache\App_Data";
            // if (!File.Exists(filePath))
            //{
            //  File.WriteAllText(filePath, "content");
            //}
          // var names=
            if (this.Cache["folder"] == null)
            {
                var dependency = new CacheDependency(folderPath);
                var content = string.Format("{0} [{1}]", FileNames(folderPath), DateTime.Now);
                Cache.Insert(
                    "folder",         // key
                    content,   // object
                    dependency,     // dependencies
                    DateTime.Now.AddHours(1), // absolute exp.
                    TimeSpan.Zero,               // sliding exp.
                    CacheItemPriority.Default,   // priority
                    null);          // callback delegate
            }

            this.filePathSpan.InnerText = folderPath;
            this.currentTimeSpan.InnerText = this.Cache["folder"] as string;
        }
        private string FileNames(string path)
        {
            var res = Directory.GetFiles(path);
            StringBuilder sb = new StringBuilder();
            foreach (var st in res)
            {
                var sss = st;
                sb.AppendLine(st);
            }
            return sb.ToString();

        }
    }
}