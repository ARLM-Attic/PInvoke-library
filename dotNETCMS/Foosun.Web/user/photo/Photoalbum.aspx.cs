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

public partial class user_Photoalbum : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        DataTable dts = pho.sel_7(Foosun.Global.Current.UserNum);
        this.Photoalbum.DataSource = dts;
        this.Photoalbum.DataTextField = "ClassName";
        this.Photoalbum.DataValueField = "ClassID";
        this.Photoalbum.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            int isDisPhotoalbum = 0;
            string DisID = "";

            string PhotoalbumName = Request.Form["PhotoalbumName"].ToString();
            string pwd = "";
            string pwds = Request.Form["pwd"].ToString();
            if (pwds != "")
            {
                pwd = Foosun.Common.Input.MD5(Request.Form["pwd"].ToString(), true);
            }
            string ClassID = this.Photoalbum.SelectedValue.ToString();
            string Jurisdiction1 = "";
            string Jurisdiction2 = "";
            string PhotoalbumJurisdiction = "";

            string UserNumfiles = Foosun.Global.Current.UserNum;
            string PhotoalbumUrl = "~/" + Userfiles + "/" + UserNumfiles + "/" + PhotoalbumName;
            if (this.Radio1.Checked)
            {
                Jurisdiction1 = "0";
                Jurisdiction2 = this.number.Text;
                PhotoalbumJurisdiction = Jurisdiction1 + "|" + Jurisdiction2;
            }
            else
            {
                Jurisdiction1 = "1";
                Jurisdiction2 = "0";
                PhotoalbumJurisdiction = Jurisdiction1 + "|" + Jurisdiction2;
            }
            Foosun.Model.STPhotoalbum Pb = new Foosun.Model.STPhotoalbum();
            Pb.ClassID = ClassID;
            Pb.DisID = DisID;
            Pb.isDisPhotoalbum = isDisPhotoalbum;
            Pb.PhotoalbumJurisdiction = PhotoalbumJurisdiction;
            Pb.PhotoalbumName = PhotoalbumName;
            Pb.PhotoalbumUrl = PhotoalbumUrl;
            Pb.pwd = pwd;

            string Dir = System.Web.HttpContext.Current.Server.MapPath
("~/" + Userfiles + "/" + UserNumfiles + "/" + PhotoalbumName).ToString();
            if (Photoalbum.SelectedValue != "")
            {
                if (System.IO.Directory.Exists(Dir))
                {
                    PageError("添加失败相册已经从在", "Photoalbumlist.aspx");
                }
                else
                {
                    if (pho.Add_1(Pb, UserNum) != 0)
                    {
                        CreateFolder(PhotoalbumName);
                        PageRight("添加成功", "Photoalbumlist.aspx");
                    }
                    else
                    {
                        PageError("添加失败", "Photoalbumlist.aspx");
                    }
                }
            }
            else
            {
                PageError("添加失败请先添加相册分类", "Photoalbumlist.aspx");
            }
        }
    }
    public void CreateFolder(string FolderPathName)
    {
        string UserNumfile = Foosun.Global.Current.UserNum;
        if (FolderPathName.Trim().Length > 0)
        {
            try
            {
                string CreatePath = System.Web.HttpContext.Current.Server.MapPath
("~/" + Userfiles + "/" + UserNumfile + "/" + FolderPathName).ToString();
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
}




