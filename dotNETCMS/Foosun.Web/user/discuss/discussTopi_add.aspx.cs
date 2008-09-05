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

public partial class user_discussTopi_add : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["timetoadd"] != null)
        {
            DateTime creatTimess = DateTime.Now;//获取当前系统时间
            try
            {
                DateTime tm = DateTime.Parse(Session["timetoadd"].ToString());
                TimeSpan st = creatTimess - tm;
                if (st.TotalSeconds < 30){PageError("请不要频繁发起主题.您的发言被抵抗.", "");}
            }
            catch{}
        }
        if (!Page.IsPostBack)
        {
            Response.CacheControl = "no-cache";
            
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
            DataTable dt_usd = dis.sel_29(DisIDs);
            int cut_um = dt_usd.Rows.Count;
             int tu=0;
             if (cut_um > 0)
             {        
                 //<--修改者：吴静岚 时间：2008-06-23 
                 for (int i = 0; i < cut_um; i++)
                 {
                     if (dt_usd.Rows[i]["UserNum"].ToString() == Foosun.Global.Current.UserNum)
                     {
                         tu = 1;
                         break;
                     }
                 }
                 //-->wjl
            }
            string[] Authority = dis.sel_34(DisIDs).Split('|');
            int Authority1 = int.Parse(Authority[1].ToString());
            if (dis.sel_30(DisIDs) != pd.getUserName(Foosun.Global.Current.UserNum))
            {
                if (Authority1 == 0)
                {
                    PageError("该组不能发表主题", "discussTopi_list.aspx?DisID=" + DisIDs + "");
                }
                else 
                {
                    if (tu == 0)
                    {
                        PageError("你没有加入该组，不能发表主题，请先加入。", "discussTopi_list.aspx?DisID=" + DisIDs+ "");
                    }
                }
            }
            sc.InnerHtml = Show_scs(DisIDs);
        }
    }
    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
            string Title = Foosun.Common.Input.Htmls(Request.Form["Title"].ToString());
            DateTime creatTime = DateTime.Now;//获取当前系统时间
            string DtID = Foosun.Common.Rand.Number(12);

            string Content = Foosun.Common.Input.Htmls(ContentBox.Value);
            DateTime voteTime = DateTime.Now;
            if (Foosun.Common.Input.IsDate(this.voteTime.Text))
            {
                voteTime = DateTime.Parse(Request.Form["voteTime"]);
            }
            else
            {
                voteTime = DateTime.Parse("2010-1-1");
            }
            int source = 0;
            string DtUrl = "";
            if (this.source2.Checked)
            {
                source = 0;
                DtUrl = this.DtUrl.Text;
            }
            else
            {
                source = 1;
                DtUrl = "";
            }
            DataTable dt = dis.sel_35();
            int cut = dt.Rows.Count;
            string DtIDss = "";
            if (cut>0)
            {
                DtIDss = dt.Rows[0]["DtID"].ToString();
            }
            if (DisID != DtIDss)
            {
                if (dis.Add_6(DtID, Title, Content, source, DtUrl, UserNum, creatTime, voteTime, DisID)==0)
                {
                    PageError("发表错误", "discussTopi_list.aspx?DisID=" + DisID + "");
                }
                else
                {
                    Session["timetoadd"] = creatTime;
                    PageRight("发表成功", "discussTopi_list.aspx?DisID=" + DisID + "");
                }
            }
            PageError("发表错误可能编号重复", "discussTopi_list.aspx?DisID=" + DisID + "");

        }
    }
        string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        scs += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussManage_list.aspx\" class=\"list_link\">讨论组管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussManage_list.aspx\" class=\"list_link\">讨论组管理</a>&nbsp;&nbsp;<a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" class=\"list_link\">讨论组主题管理</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DisID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DisID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp;</span></td></tr></table>";
        return scs;
    }
}




