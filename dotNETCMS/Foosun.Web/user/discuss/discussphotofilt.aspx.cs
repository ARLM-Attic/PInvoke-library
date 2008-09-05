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
using Foosun.CMS.Common;

public partial class user_discussphotofilt : Foosun.Web.UI.UserPage
{
    protected string sImgUrl = "";
    Photo pho = new Photo();
    protected int photocount = 0;
    protected string photostr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        sc.InnerHtml = Show_scs(DisIDs);
        getPhoto();

    }
    protected void getPhoto()
    {
        string PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"]);
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        string pwd = pho.sel_1(PhotoalbumID);
        string UserNum = pho.sel_20(PhotoalbumID);
        if (pwd != "" && pwd != null && UserNum != Foosun.Global.Current.UserNum)
        {
            Response.Redirect("discussPhotoalbumlist.aspx?DisID='" + DisIDs + "'");
        }
        int n = 0;
        DataTable dt = pho.sel_18(PhotoalbumID);
        if (dt != null)
        {
            photocount = dt.Rows.Count;
            //foreach (DataRow r in dt.Rows)
            //{
            //    if (!r.IsNull(0))
            //    {
            //        if (n > 0)
            //            sImgUrl += "\t";
            //        sImgUrl += r[0].ToString().Replace("{@UserdirFile}", Foosun.Config.UIConfig.dirDumm + "/" + Foosun.Config.UIConfig.UserdirFile);
            //        n++;
            //    }
            //}
            string dirDumm = Foosun.Config.UIConfig.dirDumm;
            if (dirDumm.Trim() != "")
                dirDumm = "/" + dirDumm;

            photostr += "ImgName = new ImgArray(" + photocount + ");\n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                photostr += "ImgName[" + i + "] =\"" + (dt.Rows[i][0].ToString().ToLower().Replace("{@userdirfile}", dirDumm + Foosun.Config.UIConfig.UserdirFile)).Replace("discuss/", "").Replace("//", "/") + "\";\n";
            }
            dt.Dispose();
        }
        DataBind();       
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return scs;
    }
}
