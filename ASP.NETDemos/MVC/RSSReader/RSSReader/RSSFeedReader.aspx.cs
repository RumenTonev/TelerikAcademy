using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.IO;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;

namespace RSSReader
{
    public partial class RSSFeedReader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("http://polling.bbc.co.uk/sport/shared/football/oppm/json/EFBO756244");

            string webData = System.Text.Encoding.UTF8.GetString(raw);
           object obj= JsonConvert.DeserializeObject(webData);
            //Loading My site RSS FEED, Change as per your needjsoi
            //    WebRequest MyRssRequest = WebRequest.Create("http://feeds.bbci.co.uk/sport/0/football/rss.xml?edition=uk#");
            //    WebResponse MyRssResponse = MyRssRequest.GetResponse();

            //    Stream MyRssStream = MyRssResponse.GetResponseStream();

            //    XmlDocument MyRssDocument = new XmlDocument();
            //    MyRssDocument.Load(MyRssStream);

            //    XmlNodeList MyRssList = MyRssDocument.SelectNodes("rss/channel/item");

            //    string sTitle = "";
            //    string sLink = "";
            //    string sDescription = "";

            //    // Iterate/Loop through RSS Feed items
            //    for (int i = 0; i < MyRssList.Count; i++)
            //    {
            //        XmlNode MyRssDetail;

            //        MyRssDetail = MyRssList.Item(i).SelectSingleNode("title");
            //        if (MyRssDetail != null)
            //            sTitle = MyRssDetail.InnerText;
            //        else
            //            sTitle = "";

            //        MyRssDetail = MyRssList.Item(i).SelectSingleNode("link");
            //        if (MyRssDetail != null)
            //            sLink = MyRssDetail.InnerText;
            //        else
            //            sLink = "";

            //        MyRssDetail = MyRssList.Item(i).SelectSingleNode("description");
            //        if (MyRssDetail != null)
            //            sDescription = MyRssDetail.InnerText;
            //        else
            //        {
            //            sDescription = "";
            //        }

            //        // Now generating HTML table rows and cells based on Title,Link & Description
            //        HtmlTableCell block = new HtmlTableCell();

            //        // You can style the Title From Here
            //        block.InnerHtml = "<span style='font-weight:bold'><a href='" + sLink + "' target='new'>" + sTitle + "</a></span>";
            //        HtmlTableRow row = new HtmlTableRow();
            //        row.Cells.Add(block);
            //        tbl_Feed_Reader.Rows.Add(row);
            //        HtmlTableCell block_description = new HtmlTableCell();

            //        //You can style the Description from here
            //        block_description.InnerHtml = "<p align='justify'>" + sDescription + "</p>";
            //        HtmlTableRow row2 = new HtmlTableRow();
            //        row2.Cells.Add(block_description);
            //        tbl_Feed_Reader.Rows.Add(row2);
            //    }
            //}
        }
    }
}