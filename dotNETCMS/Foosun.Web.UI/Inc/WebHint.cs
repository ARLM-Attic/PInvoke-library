using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Foosun.Config;
using Foosun.CMS;

namespace Foosun.Web.UI
{
    public class WebHint
    {
         /// <summary>
        /// 页面错误提示信息
        /// </summary>
        /// <param name="ErrMsg">错误信息</param>
        /// <param name="Url">返回管理员地址  默认可以填写:""或"0"</param>
        /// 更新时间2007-3-7
        static public void ShowError(string ErrMsg, string Url,bool returnUrl)
        {
            PageRender(ErrMsg, Url, false, returnUrl);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="StrUrl"></param>
        /// <returns></returns>
        static private string UserUrl(string StrUrl)
        {
            if (StrUrl.Trim() != string.Empty && StrUrl.Trim().Length>5)
            {
                StrUrl = "<a href=\"" + StrUrl + "\"><font color=\"red\">返回管理</font></a>";
            }
            return StrUrl;
        }

        /// <summary>
        /// 页面操作成功提示信息
        /// </summary>
        /// <param name="RightMsg">操作成功信息</param>
        /// <param name="Url">返回管理员地址</param>
        /// 更新时间2007-3-7
        static internal void ShowRight(string RightMsg, string Url, bool returnUrl,bool noHistory)
        {
            PageRender(RightMsg, Url, true, returnUrl,noHistory);
        }
        static internal void ShowRight(string RightMsg, string Url, bool returnUrl)
        {
            PageRender(RightMsg, Url, true, returnUrl, false);
        }
        static internal void PageRender(string Msg, string Url, bool Succeed, bool returnUrl)
        {
            PageRender(Msg, Url, Succeed, returnUrl, false);
        }
        static internal void PageRender(string Msg, string Url, bool Succeed, bool returnUrl,bool noHistory)
        {
            string cssDir = Foosun.Common.ServerInfo.GetRootURI() + "/sysImages/";
            string STitle = "操作结果!";
            string ReUrlStr = "";
            string _tmp = "<img src=\"" + cssDir + "/folder/success.gif\" border=\"0\">";
            string SCaption = "恭喜！操作成功";
            if (!Succeed)
            {
                STitle = "操作失败信息";
                _tmp = "<img src=\"" + cssDir + "/folder/error.gif\" border=\"0\">";
                SCaption = "<font color=\"red\">抱歉！操作失败</font>";
            }
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r<head>\r");
            System.Web.HttpContext.Current.Response.Write("<title>" + STitle + "_Foosun Inc.</title>\r");
            System.Web.HttpContext.Current.Response.Write("<link href=\"" + cssDir + UIConfig.CssPath() + "/css/css.css\" rel=\"stylesheet\" type=\"text/css\" />\r");
            System.Web.HttpContext.Current.Response.Write("<script src=\"" + Foosun.Common.ServerInfo.GetRootURI() + "/configuration/js/Prototype.js\" language=\"javascript\" type=\"text/javascript\"></script>\r");
            System.Web.HttpContext.Current.Response.Write("<script src=\"" + Foosun.Common.ServerInfo.GetRootURI() + "/configuration/js/Public.js\" language=\"javascript\" type=\"text/javascript\"></script>\r");
            System.Web.HttpContext.Current.Response.Write("\r</head>\r");
            if (returnUrl)
            {
                if (Url != string.Empty && Url != null)
                {
                    System.Web.HttpContext.Current.Response.Write("<body onload=\"returnPage('"+Url+"');\" style=\"margin-top:50px;\">\r");
                    ReUrlStr = "<li><span style=\"color:blue\">2秒后自动转向...</span></li>";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("<body style=\"margin-top:50px;\">\r");
            }
            System.Web.HttpContext.Current.Response.Write("    <table style=\"width:65%;height:180px;\"  border=\"0\" align=\"center\" cellspacing=\"1\" cellpadding=\"5\" class=\"table\">\r   <tr class=\"TR_BG\"><td class=\"sysmain_navi\" style=\"height:38px;\" colspan=\"2\">" + SCaption + "</td>\r");
            System.Web.HttpContext.Current.Response.Write("</tr><tr class=\"TR_BG_list\"><td class=\"list_link\" align=\"center\" style=\"40%\">" + _tmp + "<br /><br /></td><td class=\"list_link\"><font color=red>操作描述：</font>\r");
            System.Web.HttpContext.Current.Response.Write("    <ul>\r");
            if (noHistory)
            {
                System.Web.HttpContext.Current.Response.Write("        <li>" + UserUrl(Url) + "</li>" + ReUrlStr + "\r");
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("        <li><span style=\"word-wrap:bread-word;word-break:break-all;font-size:11.5px;\">" + Msg + "</span></li>\r         <li><a href='javascript:history.back();'><font color=\"red\">返回上一级</font></a>&nbsp;&nbsp;&nbsp;&nbsp;" + UserUrl(Url) + "</li>" + ReUrlStr + "\r");
            }
            System.Web.HttpContext.Current.Response.Write("     <li style=\"line-height:20px;\">" + UIConfig.returnCopyRight + "</li>\r");
            System.Web.HttpContext.Current.Response.Write("     </ul></td></tr>\r    </table>\r");
            System.Web.HttpContext.Current.Response.Write("</body>\r</html>\r");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
