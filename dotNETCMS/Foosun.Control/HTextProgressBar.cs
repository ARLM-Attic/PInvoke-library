using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Control
{
    /// <summary>
    /// 网页进度条
    /// </summary>
    public class HTextProgressBar
    {
        /// <summary>
        /// 进度条的初始化
        /// </summary>
        public static void Start()
        {
            Start("正在加载...");
        }
        /// <summary>
        /// 进度条的初始化
        /// </summary>
        /// <param name="msg">最开始显示的信息</param>
        public static void Start(string msg)
        {
            string s = "<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n";
            s += "<head>\r\n";
            s += "<title></title>\r\n";
            s += "<script language=\"javascript\" type=\"text/javascript\">\r\n";
            s += "<!--\r\n";
            //s += "var timerid;\r\n";
            //s += "var interval = 800;\r\n";
            //s += "function ShowPoint()\r\n";
            //s += "{\r\n";
            //s += "var obj = document.getElementById('TdPoint');\r\n";
            //s += "var s1 = obj.innerHTML;\r\n";
            //s += "if(s1 == '')\r\n";
            //s += "obj.innerHTML = '.';\r\n";
            //s += "else if(s1 == '.')\r\n";
            //s += "obj.innerHTML = '..';\r\n";
            //s += "else if(s1 == '..')\r\n";
            //s += "obj.innerHTML = '...';\r\n";
            //s += "else\r\n";
            //s += "obj.innerHTML = '';\r\n";
            //s += "timerid = setTimeout('ShowPoint()', 500);\r\n";
            //s += "}\r\n";
            s += "function EndPoint(s)\r\n";
            s += "{\r\n";
            //s += "clearTimeout(timerid);\r\n";
            s += "document.getElementById('TdPoint').innerHTML = '';\r\n";
            s += "SetText(s);\r\n";
            s += "}\r\n";
            s += "function SetText(s)\r\n";
            s += "{\r\n";
            s += "document.getElementById('TdText').innerHTML = s;\r\n";
            s += "}\r\n";
            s += "//-->\r\n";
            s += "</script>\r\n";
            s += "</head>\r\n";
            s += "<body>\r\n";
            s += "<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n";
            s += "<tr style=\"font-family: Verdana, Arial, Helvetica;font-size:11.5px;color: #DD5800;font-weight:bold\">\r\n";
            s += "<td width=\"70%\" id=\"TdText\" align=\"right\">" + msg + "</td>\r\n";
            s += "<td width=\"30%\" id=\"TdPoint\"><img src=\"/sysImages/folder/loading.gif\" /></td>\r\n";
            s += "</tr>\r\n";
            s += "</table>\r\n";
            s += "</body>\r\n";
            s += "</html>";
            System.Web.HttpContext.Current.Response.Write(s);
            System.Web.HttpContext.Current.Response.Flush();
        }
        /// <summary>
        /// 显示文本
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowText(string Msg)
        {
            Msg = Msg.Replace("'", "\'");
            //Msg = Msg.Replace("\"",@"\"");
            string jsBlock = "<script language=\"javascript\">SetText('" + Msg + "');</script>";
            System.Web.HttpContext.Current.Response.Write(jsBlock);
            System.Web.HttpContext.Current.Response.Flush();
        }
        /// <summary>
        /// 停止显示
        /// </summary>
        /// <param name="Msg"></param>
        public static void EndProgress(string Msg)
        {
            string jsBlock = "<script language=\"javascript\">EndPoint('" + Msg + "');</script>";
            System.Web.HttpContext.Current.Response.Write(jsBlock);
            System.Web.HttpContext.Current.Response.Flush();
        }
    }
}
