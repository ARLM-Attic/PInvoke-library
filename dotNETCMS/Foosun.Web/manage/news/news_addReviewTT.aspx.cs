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

public partial class manage_news_news_addReviewTT : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string fontColor = Request.QueryString["fontcolor"];
            string fontsize = Request.QueryString["fontsize"];
            string widhts = Request.QueryString["widhts"];
            string Imagesbgcolor = Request.QueryString["Imagesbgcolor"];
            string PageFontFamily = Request.QueryString["PageFontFamily"];
            string PageFontStyle = Request.QueryString["PageFontStyle"];
            string topFontInfo = Request.QueryString["topFontInfo"];
            if (PageFontFamily == "新宋")
            {
                PageFontFamily = "新宋体";
            }
            //showtt.InnerHtml = PageFontFamily.ToString();
            showtt.InnerHtml = showlist(fontColor.ToString(), fontsize.ToString(), widhts.ToString(), Imagesbgcolor.ToString(), PageFontFamily.ToString(), PageFontStyle.ToString(), topFontInfo.ToString());
        }
    }

    protected string showlist(string fontColor, string fontsize, string widhts, string Imagesbgcolor, string PageFontFamily, string PageFontStyle, string topFontInfo)
    {
        string _tmp = "";
        switch (PageFontStyle)
        {
            case "0":
                _tmp = "Regular";
                break;
            case "1":
                _tmp = "Bold";
                break;
            case "2":
                _tmp = "Italic";
                break;
            case "3":
                _tmp = "Underline";
                break;
            case "4":
                _tmp = "Strikeout";
                break;
        }
        string _Str = "<table style=\"width:" + int.Parse(widhts) + "px;height:100%\">\r";
        _Str += "<tr\r>";
        _Str += "<td  bgcolor=\"#" + Imagesbgcolor + "\">\r";
        _Str += "<label style=\"font-family:" + PageFontFamily + ";font-size:" + fontsize + "px;font-style:" + _tmp + "\"><font color=\"#" + fontColor + "\">" + topFontInfo + "</font></label>\r";
        _Str += "</td>\r";
        _Str += "</tr>\r";
        _Str += "</table>\r";
        return _Str;
    }
}
