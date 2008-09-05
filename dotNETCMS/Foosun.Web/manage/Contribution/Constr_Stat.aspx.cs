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
using Foosun.CMS.Common;

public partial class manage_Contribution_Constr_Stat : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_Constr_Stat()
    {
        Authority_Code = "C045";
    }
    Constr con = new Constr();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Showu_discusslist(1);
        }
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Showu_discusslist(PageIndex);
    }

      protected void Showu_discusslist(int PageIndex)
      {   
            int i, j;
            //DataTable dts = Foosun.CMS.Pagination.GetPage("manage_Contribution_Constr_Stat_1_aspx", PageIndex, 20, out i, out j, null);
            DataTable dts = con.GetPage1(PageIndex, 20, out i, out j, null);
            this.PageNavigator1.PageCount = j;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = i;
            if (dts != null && dts.Rows.Count!=0)
            {
                dts.Columns.Add("Constrnum", typeof(string));
                dts.Columns.Add("isChecknumber", typeof(string));
                dts.Columns.Add("MConstrnum", typeof(string));
                dts.Columns.Add("Operation",typeof(string));
                dts.Columns.Add("ParmConstrNums", typeof(string));
                dts.Columns.Add("UserNames", typeof(string));
                foreach (DataRow s in dts.Rows)
                {
                    int CN_cut=con.sel26(s["UserNum"].ToString());
                    if (CN_cut > 0)
                    {
                        s["Constrnum"] = CN_cut.ToString();
                    }
                    else 
                    {
                        s["Constrnum"] = "0";
                    }
                    int Check_cut = con.sel27(s["UserNum"].ToString());
                    if (Check_cut > 0)
                    {s["isChecknumber"] = Check_cut.ToString();}
                    else {s["isChecknumber"] = "0";}

                    DataTable dt_dd = con.sel28(s["UserNum"].ToString());

                    if(dt_dd.Rows.Count>0)
                    {
                        int m1 = DateTime.Now.Month-1;
                        int MC_cut = con.sel29(s["UserNum"].ToString(),m1);
                        if (MC_cut > 0){s["MConstrnum"] = MC_cut.ToString();}
                        else{s["MConstrnum"] = "0";}
                    }
                    else{s["MConstrnum"] = "0";}
                    s["ParmConstrNums"] = con.getParmConstrNum(s["UserNum"].ToString());
                    s["UserNames"] = pd.getUserName(s["UserNum"].ToString());
                    if (con.getParmConstrNum(s["UserNum"].ToString()) != 0)
                    {
                        s["Operation"] = "<a href=\"Constr_Pay.aspx?UserNum=" + s["UserNum"].ToString() + "\" class=\"menulist\">支付稿酬</a>";
                    }
                    else
                    {
                        s["Operation"] = "<a class=\"helpstyle\" title=\"稿酬为0，无须支付\">支付稿酬</a>";
                    }
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
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    
 }
