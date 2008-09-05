using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.Model;
using Foosun.CMS.Common;

namespace Foosun.Web.manage.Sys
{
    public partial class CreateCheckfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFiles();
            }
        }
        protected void GetFiles()
        {
            FileCompare fc = new FileCompare(Server.MapPath("~"), string.Empty);
            fc.GetFileList();
            List<FileComprInfo> list = fc.FileList;
            list.Sort(new FileNameComparer());
            DateTime Now = DateTime.Now;
            StreamWriter sw = null;
            string xmlfile = HttpContext.Current.Server.MapPath("./" + Now.ToString("yyyyMMddHHmmss") + ".xml");
            sw = File.CreateText(xmlfile);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
            sw.WriteLine("<rss version=\"2.0\">\r");
            sw.WriteLine("<foosun>\r");
            foreach (FileComprInfo info in list)
            {
                if (info.FaModifyTime != DateTime.MinValue)
                    sw.WriteLine("<file name=\"" + info.FileName + "\" size=\"" + info.FaFileSize + "\" modifytime=\"" + info.FaModifyTime + "\" />");
            }
            sw.WriteLine("</foosun>");
            sw.WriteLine("</rss>");
            sw.Flush();
            sw.Close(); sw.Dispose();
            //Response.End();
        }
    }
}
