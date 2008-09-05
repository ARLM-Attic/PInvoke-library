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
using System.IO;
using Foosun.CMS;
using Foosun.Model;

public partial class user_discuss_discussPhotoalbumlist : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator3.OnPageChange += new PageChangeHandler(PageNavigator3_PageChange);
        Response.CacheControl = "no-cache";

        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }

        switch (Type)
        {
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
        }

        string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        DataTable dt = dis.sel(Foosun.Global.Current.UserNum);
        DataTable dt_selDiscussMember = dis.sel_29(DisID);
        int cut_selDiscussMember = dt_selDiscussMember.Rows.Count;
        if (dis.sel_30(DisID) != dt.Rows[0]["UserName"].ToString())
        {
            if (cut_selDiscussMember == 0)
            {

                PageError("对不起你没有加入此讨论组无权进入相册", "");
            }
        }
        if (!IsPostBack)
        {
            Show_cjlist(1);
        }
    }
    protected void PageNavigator3_PageChange(object sender, int PageIndex2)
    {
        Show_cjlist(PageIndex2);
    }
    protected void Show_cjlist(int PageIndex2)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
        int ib, jb;
        SQLConditionInfo st = new SQLConditionInfo("@DisID", DisIDs);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex2, 10, out ib, out jb, st);

        this.PageNavigator3.PageCount = jb;
        this.PageNavigator3.PageIndex = PageIndex2;
        this.PageNavigator3.RecordCount = ib;
        if (cjlistdts.Rows.Count > 0)
        {
            cjlistdts.Columns.Add("idc2", typeof(string));
            cjlistdts.Columns.Add("PhotoalbumNames", typeof(string));
            cjlistdts.Columns.Add("UserNames", typeof(string));
            cjlistdts.Columns.Add("Pwd", typeof(string));
            
            foreach (DataRow h in cjlistdts.Rows)
            {
                if (h["pwd"].ToString() == "")
                {
                    h["Pwd"] = "";
                }
                else
                {
                    h["Pwd"] = "<img src=\"../../sysImages/folder/pw.gif\" alt=\"访问需要密码\" />";
                }
                h["idc2"] = "<a href=\"discussPhotoalbum_up.aspx?PhotoalbumID=" + h["PhotoalbumID"].ToString() + "&DisID=" + DisIDs + "\" class=\"list_link\">修改</a>┆<a href=\"#\" onclick=\"javascript:del('" + h["PhotoalbumID"].ToString() + "','" + DisIDs + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["PhotoalbumID"].ToString() + "  runat=\"server\" /></td>";
                h["PhotoalbumNames"] = "<a href=\"discussphoto.aspx?PhotoalbumID=" + h["PhotoalbumID"].ToString() + "&DisID=" + DisIDs + "\" class=\"list_link\">" + h["PhotoalbumName"].ToString() + "</a>";
                DataTable dts = dis.sel(h["UserName"].ToString());
                int cut = dts.Rows.Count;
                if (cut>0)
                {
                    h["UserNames"] = dts.Rows[0]["UserName"].ToString();
                }
                else 
                {
                    h["UserNames"] = "此用户被删除";
                }

            }
            sc.InnerHtml = Show_sc(DisIDs);
            Repeater1.DataSource = cjlistdts;
            Repeater1.DataBind();
        }
        else
        {
            sc.InnerHtml = Show_scs(DisIDs);
            no.InnerHtml = Show_no();
            this.PageNavigator3.Visible = false;
        }

    }
    string Show_no()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    string Show_sc(string DisID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        sc+="<tr><td height=\"1\" colspan=\"2\"></td></tr>";
    sc+=
        sc+="<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
    sc += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        sc+="<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        sc += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a>&nbsp;&nbsp;&nbsp; <a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a></td></tr></table>";
        return sc;
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
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];


        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的相册!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    DataTable dt_pu = dis.sel_31(chSplit[i]);
                    DeleParentFolder(dt_pu.Rows[0]["PhotoalbumUrl"].ToString());
                    if (dt_pu.Rows[0]["UserName"].ToString() == Foosun.Global.Current.UserNum)
                    {
                        if (pho.sel_19(chSplit[i]) != 0)
                        {
                            if (dis.Delete_10(chSplit[i]) == 0 || dis.Delete_9(chSplit[i]) == 0)
                            {
                                PageError("批量删除失败", "");
                                break;
                            }
                        }
                        else 
                        {
                            if (dis.Delete_9(chSplit[i]) == 0)
                            {
                                PageError("批量删除失败", "");
                                break;
                            }
                        }
                    }
                    else 
                    {
                        PageError("此相册不是你建立的你无权删除", "");
                    }
                     
                }
            }
            PageRight("批量删除成功", "");
        }

    }

    protected void del(string ID)
    {
        DataTable dt_pu = dis.sel_31(ID);
        DeleParentFolder(dt_pu.Rows[0]["PhotoalbumUrl"].ToString());
        if (dt_pu.Rows[0]["UserName"].ToString() == Foosun.Global.Current.UserNum)
        {
            if (pho.sel_19(ID) != 0)
            {
                if (dis.Delete_10(ID) == 0 || dis.Delete_9(ID) == 0)
                {
                    PageError("删除失败", "");
                }

                else
                {
                    PageRight("删除成功!", "");
                }
            }
            else
            {
                if (dis.Delete_9(ID) == 0)
                {
                    PageError("删除失败", "");
                }

                else
                {
                    PageRight("删除成功!", "");
                }
            }
        }
        else
        {
            PageError("此相册不是你建立的你无权删除", "");
        }
    }
    public void DeleParentFolder(string Url)
    {
        DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(Url).ToString());
        if (DelFolder.Exists)
        {
            DelFolder.Delete();
        }
    }
}