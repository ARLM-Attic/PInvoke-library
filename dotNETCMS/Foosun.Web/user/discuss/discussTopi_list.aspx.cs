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
using Foosun.Model;

public partial class user_discuss_discussTopi_list : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    public string D_anno = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";

        if (!IsPostBack)
        {
            string DisIDs = Request.QueryString["DisID"];
            if (DisIDs == null && DisIDs == string.Empty)
            {
                PageError("错误的参数", "");
            }
            dis.Update_3(Foosun.Common.Input.Filter(DisIDs.ToString()));
            DataTable dt_dts = dis.sel_40(Foosun.Common.Input.Filter(DisIDs.ToString()));
            int cut_dts = dt_dts.Rows.Count;
            if (cut_dts > 0)
            {
                if (dt_dts.Rows[0]["UserNum"].ToString() == Foosun.Global.Current.UserNum)
                {
                    sc.InnerHtml = Show_sc(Foosun.Common.Input.Filter(DisIDs.ToString()));
                }
            }
            else
            {
                sc.InnerHtml = Show_scs(Foosun.Common.Input.Filter(DisIDs.ToString()));
            }
            D_anno = dis.sel_41(Foosun.Common.Input.Filter(DisIDs.ToString()));
            Showu_discusslist(1);            
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        switch (Type)
        {
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
        }
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }
      protected void Showu_discusslist(int PageIndex)//显示所有讨论组列表
      {
          string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
          SQLConditionInfo st = new SQLConditionInfo("@DisID", DisID);
            int ig, js;
            DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out ig, out js, st);

            this.PageNavigator1.PageCount = js;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = ig;
            if (dts != null)
            {
                dts.Columns.Add("Titlea", typeof(string));
                dts.Columns.Add("UserName", typeof(string));
                dts.Columns.Add("idc", typeof(string));
                dts.Columns.Add("creatTimess", typeof(string));
                foreach (DataRow s in dts.Rows)
                {

                    DataTable dtsd = dis.sel_42(s["UserNum"].ToString());
                    int cutsd = dtsd.Rows.Count;
                    string AUT="";
                    if (cutsd>0)
                    {
                        if (s["UserNum"].ToString() == Foosun.Global.Current.UserNum)
                        {
                            AUT = "<a href=\"discussTopi_del.aspx?DtID=" + s["DtID"].ToString() + "&VoteTF=" + s["VoteTF"].ToString() + "\"  class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["DtID"].ToString() + "  runat=\"server\" />";
                        }
                        else
                        {
                            AUT = "无操作";
                        }
                        s["UserName"] = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/ShowUser.aspx?uid=" + dtsd.Rows[0]["UserName"].ToString() + "\" target=\"_blank\" class=\"list_link\">" + dtsd.Rows[0]["UserName"].ToString() + "</a>";
                    }
                    else
                    {
                        AUT = "无操作";
                        s["UserName"] = "此用户已被删除";
                    }
                    s["Titlea"] = "<a href=\"discussTopi_commentary.aspx?DtID=" + s["DtID"].ToString() + "&DisID=" + DisID + "\"  class=\"list_link\">" + s["Title"].ToString() + "</a>";
                    s["idc"] =" " + AUT +  "";
                    s["creatTimess"] = DateTime.Parse(s["creatTime"].ToString());
                 
                }

            }
                  
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
            DataList1.DataSource = dts;
            DataList1.DataBind();
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
    string Show_sc(string DidID)
    {
        string sc = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        sc+="<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        sc += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussManage_list.aspx\" class=\"list_link\">讨论组管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        sc += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussTopi_list.aspx?DisID=" + DidID + "\" class=\"list_link\">讨论组主题管理</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DidID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DidID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp; <a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>&nbsp;&nbsp;<input type=\"checkbox\" name=\"Checkbox1\" onclick=\"javascript:selectAll(this.form,this.checked)\" /></span></td></tr></table>";
        return sc;
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\"><tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >讨论组主题管理</td><td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" >";
        scs += "<div align=\"left\">位置导航：<a href=\"../main.aspx\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussManage_list.aspx\" class=\"list_link\">讨论组管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" class=\"list_link\">讨论组主题管理</a></div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\"><tr><td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussTopi_list.aspx?DisID=" + DisID + "\" class=\"list_link\">讨论组主题管理</a>&nbsp;&nbsp;<a href=\"discussTopi_add.aspx?DisID=" + DisID + "\" class=\"menulist\">发表主题</a>&nbsp;&nbsp;<a href=\"discussTopi_ballot.aspx?DisID=" + DisID + "\" class=\"menulist\">发起投票</a>&nbsp;&nbsp;</span></td></tr></table>";
        return scs;
    }
    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的主题!", "");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {  
                    if (dis.Delete_11(chSplit[i]) == 0 || dis.Delete_12(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败", "");
                        break;
                        
                    }
                }
            }
            PageRight("批量删除成功", "Constr_List.aspx");
        }

    }
}

