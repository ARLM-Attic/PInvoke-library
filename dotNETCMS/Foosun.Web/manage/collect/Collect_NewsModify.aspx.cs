//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
//===========================================================
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
using System.Net;
using System.Text.RegularExpressions;
using Foosun.Model;

public partial class manage_collect_Collect_NewsModify : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim().Equals(""))
            {
                PageError("参数不正确或无效!", "");
                return;
            }
            this.TxtClassName.Attributes.Add("readonly", "true");
            int id = int.Parse(Request.QueryString["ID"].Trim());
            Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
            this.HidNewsID.Value = id.ToString();
            DataTable tb = cl.SiteList();
            if (tb != null)
            {
                this.DdlSite.DataTextField = "SiteName";
                this.DdlSite.DataValueField = "ID";
                this.DdlSite.DataSource = tb;
                this.DdlSite.DataBind();
                tb.Dispose();
            }
            CollectNewsInfo info = cl.GetNews(id);
            this.TxtTitle.Text = info.Title;
            this.TxtLink.Text = info.Links;
            this.DdlSite.SelectedValue = info.SiteID.ToString();
            this.TxtAuthor.Text = info.Author;
            this.TxtSource.Text = info.Source;
            this.TxtDate.Text = info.AddDate.ToString();
            this.EdtContent.Value = info.Content;
            this.LblClTime.Text = info.CollectTime.ToString();
            this.HidClassID.Value = info.ClassID;
            Foosun.CMS.ContentManage cm = new Foosun.CMS.ContentManage();
            string ClassName = cm.getClassCName(this.HidClassID.Value);
            this.TxtClassName.Text = ClassName;
        }
    }
    protected void BtnOK_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int id = int.Parse(HidNewsID.Value);
            int site = int.Parse(DdlSite.SelectedValue);
            if (TxtTitle.Text.Trim().Equals(""))
            {
                PageError("标题不能为空!", "");
            }
            if (TxtLink.Text.Trim().Equals(""))
            {
                PageError("链接地址不能为空!", "");
            }
            if (this.EdtContent.Value.Trim().Equals(""))
            {
                PageError("新闻内容不能为空!", "Collect_News.aspx");
            }
            if (this.HidClassID.Value.Trim().Equals(""))
            {
                PageError("新闻入库后的栏目不能为空!", "Collect_News.aspx");
            }
            CollectNewsInfo info = new CollectNewsInfo();
            if (!this.TxtDate.Text.Trim().Equals(""))
            {
                try
                {
                    info.AddDate = Convert.ToDateTime(this.TxtDate.Text);
                }
                catch
                {
                    PageError("采集日期格式不正确!", "Collect_News.aspx");
                }
            }
            else
            {
                info.AddDate = DateTime.Now;
            }
            info.SiteID = site;
            info.Title = this.TxtTitle.Text;
            info.Source = this.TxtSource.Text;
            info.Author = this.TxtAuthor.Text;
            info.Content = this.EdtContent.Value;
            info.Links = this.TxtLink.Text.Trim();
            info.ClassID = this.HidClassID.Value;
            Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
            cl.NewsUpdate(id, info);
            PageRight("修改新闻成功!", "Collect_News.aspx");
        }
    }
}
