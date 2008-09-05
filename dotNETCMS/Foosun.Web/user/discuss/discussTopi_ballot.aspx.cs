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

public partial class user_discussTopi_ballot : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();    
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (Session["time"] != null)
            {
                DateTime creatTimess = DateTime.Now;//获取当前系统时间
                try
                {
                    DateTime tm = DateTime.Parse(Session["time"].ToString());
                    TimeSpan st = creatTimess - tm;
                
                    if (st.TotalSeconds < 30)
                    {
                        PageError("对不起.请不要频繁发起投票！", "");
                    }
                }
                catch
                { 
                    
                }               
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
                //<--修改者：吴静岚 时间：2008-06-24 解决加入组后仍然不能发起投票问题
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
            if (Authority1 == 0)
            {
                PageError("对不起该组不能发表主题", "discussTopi_list.aspx?DisID=" + DisIDs + "");
            }
            else 
            {
                if (tu == 0)
                {
                    PageError("对不起你没有加入该组，不能发表主题，请先加入", "discussTopi_list.aspx?DisID=" + DisIDs + "");
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
            string tbhead = Foosun.Config.UIConfig.dataRe;
            string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
            string Title = Foosun.Common.Input.Filter(Request.Form["Title"].ToString());
            DateTime creatTime = DateTime.Now;//获取当前系统时间
            string DtID = Foosun.Common.Rand.Number(12);
            string Content = Foosun.Common.Input.Htmls(ContentBox.Value);
            string Votit = Request.Form["Voteitem"].ToString();
            string[] Voteitem = Votit.Split('\n');
            DateTime voteTime = DateTime.Now;
            if (this.voteTime.Text.Trim() != "")
            {
                voteTime = DateTime.Parse(Foosun.Common.Input.Filter(Request.Form["voteTime"].ToString()));
            }
            else
            {
                voteTime = DateTime.Parse("3000-1-1");
            }
            string votegenre=this.votegenresds.SelectedValue;
            DataTable dt_DtIDs = dis.sel_35();
            int cut = dt_DtIDs.Rows.Count;
            string DtIDss = "";
            if (cut>0)
            {
                DtIDss = dt_DtIDs.Rows[0]["DtID"].ToString();
            }
            if (DisID != DtIDss)
            {
                if (dis.Add_7(DtID, Title, Content, UserNum, creatTime, voteTime, DisID)==0)
                {
                    PageError("发表错误", "discussTopi_list.aspx?DisID=" + DisID + "");
                }
                else
                {
                    for (int x = 0; x < Voteitem.Length; x++)
                    {
                        string VoteID = Foosun.Common.Rand.Number(12);
                        while (dis.sel_36(VoteID) > 0)
                        {
                            VoteID = Foosun.Common.Rand.Number(12);
                        }
                        bool flag = dis.Add_8(DtID, VoteID, votegenre, Voteitem[x].ToString());
                    }
                }
                Session["time"] = creatTime;
                PageRight("发表成功", "");              
            }
            PageError("发表错误可能编号重复", "discussTopi_list.aspx?DisID=" + DisID + "");
        }
    }
        string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        scs += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" target=\"sys_main\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussManage_list.aspx\" class=\"list_link\">讨论组管理</a>&nbsp;&nbsp;<a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" target=\"sys_main\" class=\"list_link\">讨论组主题管理</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DisID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DisID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp;</span></td></tr></table>";
        return scs;
    }
}