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
using System.IO;

public partial class user_discussPhotoalbum : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    public string dimm = Foosun.Config.UIConfig.dirDumm;
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (dimm.Trim() != string.Empty)
        {
            dimm = "/" + dimm;
        }
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        DataTable dts = dis.sel_27(DisIDs);
        this.Photoalbum.DataSource = dts;
        this.Photoalbum.DataTextField = "ClassName";
        this.Photoalbum.DataValueField = "ClassID";
        this.Photoalbum.DataBind();
        sc.InnerHtml = Show_scs(DisIDs);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            int isDisPhotoalbum = 1;
            string DisID = Request.QueryString["DisID"].ToString();

            string PhotoalbumName = Request.Form["PhotoalbumName"].ToString();

            string pwd = "";
            string pwds=Request.Form["pwd"].ToString();
            if (pwds != "")
            {
                pwd = Foosun.Common.Input.MD5(Request.Form["pwd"].ToString(), true);
            }
            string ClassID = this.Photoalbum.SelectedValue.ToString();
            string Jurisdiction1="";
            string Jurisdiction2="";
            string PhotoalbumJurisdiction="";

            string UserNumfiles = Foosun.Global.Current.UserNum;
            string PhotoalbumUrl = "/" + Userfiles + "/discuss/" + DisID + "/" + Foosun.Global.Current.UserNum + "/" + PhotoalbumName;
                
            if(this.Radio1.Checked)
            {
                 Jurisdiction1="0";
                 Jurisdiction2=this.number.Text;
                PhotoalbumJurisdiction=Jurisdiction1+"|"+Jurisdiction2;
            }
            else
            {
                Jurisdiction1="1";
                Jurisdiction2="0";
                PhotoalbumJurisdiction=Jurisdiction1+"|"+Jurisdiction2;
            }

            Foosun.Model.STPhotoalbum Pb = new Foosun.Model.STPhotoalbum();
            Pb.ClassID = ClassID;
            Pb.DisID = DisID;
            Pb.isDisPhotoalbum = isDisPhotoalbum;
            Pb.PhotoalbumJurisdiction = PhotoalbumJurisdiction;
            Pb.PhotoalbumName = PhotoalbumName;
            Pb.PhotoalbumUrl = PhotoalbumUrl;
            Pb.pwd = pwd;

            string Dir = System.Web.HttpContext.Current.Server.MapPath(dimm + "/" + Userfiles + "/discuss/" + DisID + "/" + UserNumfiles + "/" + PhotoalbumName).ToString();

            if (System.IO.Directory.Exists(Dir))
            {
                PageError("添加失败相册已经从在<br>", "discussPhotoalbumlist.aspx?DisID=" + DisID + "");
            }
            else
            {
                if (pho.Add_1(Pb,UserNum)!=0)
                {
                    CreateFolder(PhotoalbumName);
                    PageRight("添加成功", "discussPhotoalbumlist.aspx?DisID=" + DisID + "");
                }
                else
                {
                    PageError("添加失败<br>", "discussPhotoalbumlist.aspx?DisID=" + DisID + "");
                }
            }
        }
    }

    public void CreateFolder(string FolderPathName)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        string UserNumfile = Foosun.Global.Current.UserNum;
        if (FolderPathName.Trim().Length > 0)
        {
            try
            {
                string CreatePath = System.Web.HttpContext.Current.Server.MapPath
("~/" + Userfiles + "/discuss/" + DisIDs + "/" + UserNumfile + "/" + FolderPathName).ToString();
                if (!Directory.Exists(CreatePath))
                {
                    Directory.CreateDirectory(CreatePath);
                }
            }
            catch
            {
                throw;
            }
        }
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />添加相册</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return scs;
    }
}




