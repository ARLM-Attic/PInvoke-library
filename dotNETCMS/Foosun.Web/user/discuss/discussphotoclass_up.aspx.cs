//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
//=====================================================================
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
using Foosun.CMS;

public partial class user_discuss_discussphotoclass_up : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
            sc.InnerHtml = Show_sc(DisIDs);
            string ClassID = Foosun.Common.Input.Filter(Request.QueryString["ClassID"].ToString());
            this.ClassName.Text = pho.sel_17(ClassID);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        DateTime Creatime = DateTime.Now;
        string ClassName = Foosun.Common.Input.Filter(Request.Form["ClassName"].ToString());
        string ClassIDs = Foosun.Common.Input.Filter(Request.QueryString["ClassID"].ToString());
        if (pho.Update_3(ClassName, Creatime, ClassIDs) != 0)
        {
            PageRight("修改分类成功", "discussphotoclass.aspx?DisID=" + DisIDs + "");
        }
        else 
        {
            PageError("修改失败<br>", "discussphotoclass.aspx?DisID=" + DisIDs + "");
        }
    }
    string Show_sc(string DisID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        sc += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc +=
            sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理<img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册分类</div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "  <tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"Photoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>&nbsp;&nbsp; <a href=\"photoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"photoclass_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加分类</a>&nbsp;&nbsp; </span><span id=\"sc\" runat=\"server\"></span></td></tr></table>";
        return sc;
    }
}



