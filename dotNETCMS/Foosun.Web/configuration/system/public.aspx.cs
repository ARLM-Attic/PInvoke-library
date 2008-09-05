using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Foosun.Web.configuration.system
{
    public partial class _public : Foosun.Web.UI.DialogPage
    {
        public _public()
        {
            BrowserAuthor = EnumDialogAuthority.ForAdmin;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string pstr = "";
            string nowstr = Request.QueryString["NowStr"];
            int intyear = DateTime.Now.Year;
            int intmonth = DateTime.Now.Month;
            int intday = DateTime.Now.Day;
            if (nowstr != null && nowstr != string.Empty)
            {
                intyear = int.Parse(nowstr.Split('-')[0].ToString());
                intmonth = int.Parse(nowstr.Split('-')[1].ToString());
                intday = int.Parse(nowstr.Split('-')[2].ToString());
            }
            pstr += "<select name=\"yearstr\">\r";
            for (int i = DateTime.Now.Year; i >= 2004; i--)
            {
                if (i == intyear)
                {
                    pstr += "<option value=\"" + i + "\" selected>" + i + "</option>\r";
                }
                else
                {
                    pstr += "<option value=\"" + i + "\">" + i + "</option>\r";
                }
            }
            pstr += "</select>年&nbsp;\r";
            pstr += "<select name=\"monthstr\">\r";
            for (int j = 1; j <= 12; j++)
            {
                if (j == intmonth)
                {
                    pstr += "<option value=\"" + j + "\" selected>" + j + "</option>\r";
                }
                else
                {
                    pstr += "<option value=\"" + j + "\">" + j + "</option>\r";
                }
            }
            pstr += "</select>月&nbsp;\r";
            pstr += "<select name=\"daystr\">\r";
            for (int m = 1; m <= 31; m++)
            {
                if (m == intday)
                {
                    pstr += "<option value=\"" + m + "\" selected>" + m + "</option>\r";
                }
                else
                {
                    pstr += "<option value=\"" + m + "\">" + m + "</option>\r";
                }
            }
            pstr += "</select>日&nbsp;<input type=\"button\" name=\"Submit\" style=\"font-size:12px;\" value=\"查询\" />\r";
            pstr += "&nbsp;<img src=\"" + Foosun.Publish.CommonData.SiteDomain + "/sysImages/folder/daylist.gif\" border=\"0\" alt=\"选择日期查看\" onclick=\"getDaylist();\" style=\"cursor:pointer;\" />\r";
            Response.Write(pstr);
        }
    }
}
