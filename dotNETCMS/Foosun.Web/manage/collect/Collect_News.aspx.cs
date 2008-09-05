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
using System.Text.RegularExpressions;

public partial class manage_collect_Collect_News : Foosun.Web.UI.ManagePage
{
    public manage_collect_Collect_News()
    {
        Authority_Code = "S012";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["Option"] != null && !Request.Form["Option"].Trim().Equals("")
            && Request.Form["NewsID"] != null && !Request.Form["NewsID"].Trim().Equals(""))
        {
            try
            {
                Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
                string id = Request.Form["NewsID"];
                if (Request.Form["Option"].Equals("DeleteNews"))
                {
                    //调用删除图片方法
                    deleteNews(id);
                    //执行删除新闻方法
                    cl.NewsDelete(id);
                    Response.Write("1%成功删除指定新闻!");
                }
            }
            catch (Exception ex)
            {
                Response.Write("0%" + ex.Message);
            }
            Response.End();
            return;
        }

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_OnPageChange);
        if (!Page.IsPostBack)
        {
            ListDataBind(1);
        }
    }
    protected void PageNavigator1_OnPageChange(object sender, int PageIndex)
    {
        ListDataBind(PageIndex);
    }
    private void ListDataBind(int PageIndex)
    {
        int nRCount, nPCount;
        Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
        this.RptNews.DataSource = cl.GetNewsPage(PageIndex, PAGESIZE, out nRCount, out nPCount);
        this.RptNews.DataBind();
        this.PageNavigator1.PageCount = nPCount;
        this.PageNavigator1.RecordCount = nRCount;
        this.PageNavigator1.PageIndex = PageIndex;
    }
    protected void RptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = (Label)e.Item.FindControl("LblState");
            if (lbl != null)
            {
                try
                {
                    bool flag = Convert.ToBoolean(lbl.Text);
                    if (flag)
                    {
                        lbl.Text = "已入库";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lbl.Text = "未入库";
                    }
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// 删除新闻
    /// </summary>
    /// <returns></returns>
    private bool deleteNews(string delID)
    {
        Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
        //得到要删除的新闻ID
        string[] strDelID = delID.Split(',');

        Foosun.Model.CollectNewsInfo collectNews = null;
        ArrayList arr = new ArrayList();
        foreach (string s in strDelID)
        {
            collectNews = cl.GetNews(Convert.ToInt32(s));
            //查询该新闻是否是已入库,如果入库则不删除图片
            if (collectNews.History)
            {
                //进入下一次循环
                continue;
            }
            else
            {
                string newsContent = collectNews.Content;
                Regex r = null;
                Match m = null;
                r = new Regex("src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                for (m = r.Match(newsContent); m.Success; m = m.NextMatch())
                {
                    arr.Add(m.Groups[1].ToString());
                }
            }
        }
        //进行删除文件
        return deleteImageFile(arr);
    }

    /// <summary>
    /// 删除图片文件
    /// </summary>
    /// <returns></returns>
    private bool deleteImageFile(ArrayList arr)
    {
        try
        {
            foreach (string s in arr)
            {
                string imageFileSrc = s.Substring(s.IndexOf("/files"),s.Length - s.IndexOf("/files"));
                Foosun.Common.Public.DelFile(null, Server.MapPath(imageFileSrc));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
