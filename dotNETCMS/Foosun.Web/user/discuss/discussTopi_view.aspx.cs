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

public partial class user_discussTopi_view : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        sc.InnerHtml = Show_sc(DisID);
        if (!IsPostBack)
        {            
            string DtID = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
            view.InnerHtml = Show_view(DtID);
         }
    }
    string Show_view(string DtIDa)
    {
        decimal votez = (decimal)dis.sel_43(DtIDa);
        DataTable dt_V = dis.sel_38(DtIDa);
        string helpTempStr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\">";
        int Cnt_V = dt_V.Rows.Count;
        for (int i = 0; i < Cnt_V; i++)
        {
            string VoteID = dt_V.Rows[i]["VoteID"].ToString();
            string Voteitem = dt_V.Rows[i]["Voteitem"].ToString();
            decimal VoteNum = decimal.Parse(dt_V.Rows[i]["VoteNum"].ToString());
            if (votez == 0)
            {
                votez++;
            }
            decimal h=(VoteNum/votez)*100;
            string hi = h.ToString();
            if (hi.Length > 3)
            {
                hi = hi.Substring(0, 3);
            }

            helpTempStr = helpTempStr +"<tr class=\"TR_BG_list\">";
            helpTempStr = helpTempStr +"<td class=\"list_link\" align='left' width=\"20%\">"+Voteitem+"</td>";
            helpTempStr = helpTempStr + "<td class=\"list_link\" align='left' width=\"80%\"><img src=\"../../sysImages/user/vote.gif\" width=\""+hi+"%\" height=\"10px\"/></td>";
            helpTempStr = helpTempStr +"</tr>";
        }  
        helpTempStr = helpTempStr +"</table>";
        return helpTempStr;
    }

    string Show_sc(string DidIDs)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        sc += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DidIDs + "\" target=\"sys_main\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussTopi_add.aspx?DisID=" + DidIDs + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DidIDs + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp; </span></td></tr></table>";
        return sc;
    }
 }

