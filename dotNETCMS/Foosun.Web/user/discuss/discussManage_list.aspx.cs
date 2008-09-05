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

public partial class user_discuss_discussManage_list : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            Showu_discusslist(1);
        }
    }
    #endregion
    /// <summary>
    /// 数据绑定分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="PageIndex"></param>
    /// 
    #region 数据绑定分页
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }
      protected void Showu_discusslist(int PageIndex)//显示所有讨论组列表
      {   
            int i, j;
            DataTable dts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, null);
            this.PageNavigator1.PageCount = j;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = i;
            if (dts != null && dts.Rows.Count!=0)
            {
                dts.Columns.Add("cutDisID", typeof(string));
                dts.Columns.Add("idc", typeof(string));
                dts.Columns.Add("Cnames", typeof(string));
                dts.Columns.Add("UserNames", typeof(string));
                
                DataTable selectDisID = dis.sel_6();
                int p;
                foreach (DataRow s in dts.Rows)
                {
                    p = (int)selectDisID.Compute("Count(DisID)", "DisID='" + s["DisID"].ToString() + "'");
                    string[] Aut = s["Authoritymoney"].ToString().Split('|');
                    int Aut1 = int.Parse(Aut[0].ToString());
                    int Aut2 = int.Parse(Aut[1].ToString());
                    int Aut3 = int.Parse(Aut[2].ToString());
                    string AUT = "";
                    if (Aut1 == 2)
                    {
                        AUT = "<a class=\"list_link\" href=\"discuss_Manageadd.aspx?DisID=" + s["DisID"].ToString() + "\" onclick=\"{if(confirm('次组需要金币或积分才能加入你确认加入吗')){return true;}return false;}\">加入</a>";

                    }
                    else if (Aut1 == 1)
                    {
                        AUT = "<a class=\"list_link\" href=\"discuss_Manageadd.aspx?DisID=" + s["DisID"].ToString() + "\" onclick=\"javascript:alert('该组拒绝任何人加入');return false;\">加入</a>";
                    }
                    else
                    {
                        AUT = "<a class=\"list_link\" href=\"discuss_Manageadd.aspx?DisID=" + s["DisID"].ToString() + "\">加入</a>";
                    }
                    s["cutDisID"] = p;
                    //s["idc"] = "<a class=\"list_link\" href=\"discussManage_DC.aspx?DisID=" + s["DisID"].ToString() + "\")\">查看</a>&nbsp;┆&nbsp;<a class=\"list_link\" href=\"disFundwarehouse.aspx?DisID=" + s["DisID"].ToString() + "\">捐献</a>&nbsp;┆&nbsp;<a class=\"list_link\" href=\"discussPhotoalbumlist.aspx?DisID=" + s["DisID"].ToString() + "\">相册</a>&nbsp;┆&nbsp;" + AUT + " ";
                    s["idc"] = "<a class=\"list_link\" href=\"discussManage_DC.aspx?DisID=" + s["DisID"].ToString() + "\")\">查看</a>&nbsp;┆&nbsp;<a class=\"list_link\" href=\"disFundwarehouse.aspx?DisID=" + s["DisID"].ToString() + "\">捐献</a>&nbsp;┆&nbsp;" + AUT + " ";
                    s["Cnames"] = "<a class=\"list_link\" href=\"discussTopi_list.aspx?DisID=" + s["DisID"].ToString() + "\")\">" + s["Cname"].ToString() + "</a>";
                    s["UserNames"] = "<a href=\"../ShowUser.aspx?uid=" + s["UserName"] + "\" target=\"_blank\" class=\"list_link\">" + s["UserName"] + "</a>";
                }
            DataList1.DataSource = dts;
            DataList1.DataBind();
            }            
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
    }
    #endregion 
    /// <summary>
    /// 前台输出
    /// </summary>
    /// <returns></returns>
    /// 
    #region 前台输出
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有投稿</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion
}