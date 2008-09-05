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

public partial class user_discuss_discussTopi_commentaryEdit : Foosun.Web.UI.UserPage
{
    Foosun.CMS.Discuss rd = new Foosun.CMS.Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string DtID = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
            string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
            if (DisID != null && DisID != "")
            {
                sc.InnerHtml = Show_sc(DtID, DisID);
                DataTable dt = rd.getTopicinfo(DtID);
                if(dt!=null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.title.Text = dt.Rows[0]["title"].ToString();
                        if (dt.Rows[0]["title"].ToString().Trim() == "")
                        {
                            this.titleTF.Visible = false;
                        }
                        this.contentBox.Value = dt.Rows[0]["Content"].ToString();
                        this.DtIDs.Value = DtID;
                    }
                    dt.Clear(); dt.Dispose();
                }
            }
            else
            {
                PageError("错误的参数", "");
            }
        }
    }


    protected string Show_sc(string DidID, string DisID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        sc += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td style=\"padding-left:14px;\"><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DidID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DidID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp;<a href=\"discussTopi_commentary.aspx?DtID=" + DidID + "&DisID=" + DisID + "\" class=\"list_link\">返回帖子</a></td></tr></table>";
        return sc;
    }

    protected void subset_Click(object sender, EventArgs e)
    {
        string title = this.title.Text;
        string Content = Foosun.Common.Input.Htmls(this.contentBox.Value);
        Content += "<div style=\"width:100%;text-align:right;\"><font color=\"#999999\">此帖子已经被作者于" + DateTime.Now + "编辑过</font></div>";
        string dtID = this.DtIDs.Value;
        if (Content == "")
        {
            PageError("请填写内容", "");
        }
        else
        {
            rd.updateTopicDtID(dtID, title, Content);
            PageRight("修改帖子成功", "discussTopi_commentary.aspx?DtID=" + Request.QueryString["DtID"] + "&DisID=" + Request.QueryString["DisID"] + "");
        }
    }
}
