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

public partial class manage_collect_Collect_StepTwo : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.EdtList.SetTextAreaRows = 7;
            this.EdtList.SetTag = new string[]{"[列表内容]","[变量]"};
            this.EdtList.SetMaxLength = 4000;
            this.EdtPageFlag.SetTag = new string[] { "[其他页面]", "[变量]" };
            this.EdtPageIndex.SetTag = new string[] { "[页码]" };
            this.EdtPageFlag.SetMaxLength = 4000;
            this.EdtPageIndex.SetMaxLength = 4000;
            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim().Equals(""))
            {
                PageError("没有传送必要的参数", "");
            }
            else
            {
                int n = int.Parse(Request.QueryString["ID"]);
                Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
                DataTable tb = cl.GetSite(n);
                if (tb == null || tb.Rows.Count < 1)
                {
                    PageError("没有找到指定的采集站点记录!", "");
                }
                this.HidSiteID.Value = n.ToString();
                int nPageType = 0;
                if (!tb.Rows[0].IsNull("OtherType"))
                    nPageType = int.Parse(tb.Rows[0]["OtherType"].ToString());
                switch (nPageType)
                {
                    case 1:
                        this.RadPageFlag.Checked = true;
                        this.EdtPageFlag.Text = tb.Rows[0]["OtherPageSetting"].ToString();
                        break;
                    case 2:
                        this.RadPageSingle.Checked = true;
                        this.EdtPageFlag.Text = tb.Rows[0]["OtherPageSetting"].ToString();
                        break;
                    case 3:
                        this.RadPageIndex.Checked = true;
                        this.EdtPageIndex.Text = tb.Rows[0]["OtherPageSetting"].ToString();
                        this.TxtPageStart.Text = tb.Rows[0]["StartPageNum"].ToString();
                        this.TxtPageEnd.Text = tb.Rows[0]["EndPageNum"].ToString();
                        break;
                    default:
                        this.RadPageNone.Checked = true;
                        break;
                }
                if (!tb.Rows[0].IsNull("ListSetting"))
                    this.EdtList.Text = tb.Rows[0]["ListSetting"].ToString();
                tb.Dispose();
            }
        }
    }
    protected void BtnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (this.EdtList.Text.IndexOf("[列表内容]") < 0)
            {
                PageError("请指定列表内容", "");
            }
            Foosun.Model.CollectSiteInfo nf = new Foosun.Model.CollectSiteInfo();
            nf.ID = int.Parse(this.HidSiteID.Value);
            nf.ListSetting = EdtList.Text;

            #region 分页设定
            nf.StartPageNum = -1;
            nf.EndPageNum = -1;
            nf.OtherType = 0;
            if (this.RadPageFlag.Checked)
            {
                if (EdtPageFlag.Text.IndexOf("[其他页面]") < 0)
                    PageError("您已经设置分页方式为递归分页,请设置分页规则(必须包含\"[其他页面]\"标识)!", "");
                nf.OtherType = 1;
                nf.OtherPageSetting = EdtPageFlag.Text;
            }
            else if (this.RadPageSingle.Checked)
            {
                if (EdtPageFlag.Text.IndexOf("[其他页面]") < 0)
                    PageError("您已经设置分页方式为单页分页,请设置分页规则(必须包含\"[其他页面]\"标识)!", "");
                nf.OtherType = 2;
                nf.OtherPageSetting = EdtPageFlag.Text;
            }
            else if (this.RadPageIndex.Checked)
            {
                nf.OtherType = 3;
                if (EdtPageIndex.Text.IndexOf("[页码]") < 0)
                    PageError("您已经设置分页方式为索引分页,请设置索引规则(必须包含\"[页码]\"标识)!", "");
                if (this.TxtPageStart.Text.Trim().Equals("") || this.TxtPageEnd.Text.Trim().Equals(""))
                    PageError("您已经设置分页方式为索引分页,请设置开始页码和结束页码!", "");

                nf.OtherPageSetting = EdtPageIndex.Text;
                nf.StartPageNum = int.Parse(TxtPageStart.Text);
                nf.EndPageNum = int.Parse(TxtPageEnd.Text);
            }
            #endregion 分页设定
            Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
            cl.SiteUpdate(nf, 2);
            Response.Redirect("Collect_StepThree.aspx?ID=" + this.HidSiteID.Value);
        }
    }

    protected void RadPageSingle_CheckedChanged(object sender, EventArgs e)
    {

    }
}
