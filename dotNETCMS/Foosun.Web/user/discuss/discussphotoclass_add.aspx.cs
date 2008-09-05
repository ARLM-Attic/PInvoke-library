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

public partial class user_discuss_discussphotoclass_add : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        if (!IsPostBack)
        {
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
            sc.InnerHtml = Show_scs(DisIDs);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        int isDisclass = 1;     
        DateTime Creatime = DateTime.Now;
        string ClassID = Foosun.Common.Rand.Number(12);
        string ClassName = Foosun.Common.Input.Htmls(Request.Form["ClassName"].ToString());

        if (Page.IsValid)
        {
            if (pho.Add_2(ClassName, ClassID, Creatime, UserNum, isDisclass, DisID) != 0)
            {
                PageRight("添加分类成功", "discussphotoclass.aspx?DisID=" + DisID + "");
            }
            else 
            {
                PageError("添加失败","");
            }
        }
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册分类</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "  <tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>&nbsp;&nbsp; <a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussphotoclass_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加分类</a>&nbsp;&nbsp;&nbsp; </span></td></tr></table>";
        return scs;
    }
}



