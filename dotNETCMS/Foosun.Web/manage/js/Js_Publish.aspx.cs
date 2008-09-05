using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;

namespace Foosun.Web.manage.js
{
    public partial class Js_Publish : Foosun.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ids = Request.QueryString["ids"];
            if (string.IsNullOrEmpty(ids))
            {
                Response.Write("没有任何要更新的js");
                Response.End();
            }
            int sucNum = 0;
            int errNum = 0;
            string[] ida = ids.Split(',');
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            for (int i = 0; i < ida.Length; i++)
            {
                try
                {
                    int tid = Convert.ToInt32(ida[i]);
                    NewsJSInfo nji = nj.GetSingle(tid);
                    nj.EstablishJsFile(nji);
                    sucNum++;
                }
                catch
                {
                    errNum++;
                }
            }
            Response.Write(String.Format("成功更新{0}条，失败{1}条", sucNum, errNum));
            Response.End();
        }
    }
}
