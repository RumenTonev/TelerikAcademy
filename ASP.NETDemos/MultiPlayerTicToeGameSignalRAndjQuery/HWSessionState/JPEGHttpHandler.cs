using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace HWSessionState
{
    public class JPEGHttpHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.RawUrl.Contains(".img"))
            {


                HttpResponse response = context.Response;
                response.ContentType = "image/jpeg";
                string Text = context.Request.QueryString["Text"];
                if (String.IsNullOrWhiteSpace(Text))
                {
                    Text = "Empty";
                }
                Color FontColor = Color.Blue;
                Color BackColor = Color.White;
                String FontName = "Times New Roman";
                int FontSize = 100;
                int Height = 150;
                int Width = 150;

                Bitmap bitmap = new Bitmap(Width, Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                Color color = Color.Gray; ;
                Font font = new Font(FontName, FontSize);
                PointF point = new PointF(75.0F, 75.0F);
                SolidBrush BrushForeColor = new SolidBrush(FontColor);
                SolidBrush BrushBackColor = new SolidBrush(BackColor);
                Pen BorderPen = new Pen(color);
                Rectangle displayRectangle = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
                graphics.FillRectangle(BrushBackColor, displayRectangle);
                graphics.DrawRectangle(BorderPen, displayRectangle);
                //Define string format 
                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
                StringFormat format2 = new StringFormat(format1);
                //Draw text string using the text format
                graphics.DrawString(Text, font, Brushes.Red, (RectangleF)displayRectangle, format2);

                bitmap.Save(response.OutputStream, ImageFormat.Jpeg);
            }

        }

        public bool IsReusable
        {
            // Return true to keep the handler in memory (pooling).
            get { return false; }
        }

        #endregion
    }
}
