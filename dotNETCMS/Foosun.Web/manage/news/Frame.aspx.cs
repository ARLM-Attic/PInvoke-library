//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                        Forum:bbs.foosun.net                     ==
//==                       WebSite:www.foosun.net                    ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                     Tel:86-28-85098980/66026180                 ==
//==                     QQ:655071,MSN:ikoolls@gmail.com             ==
//==                     Email:Service@foosun.cn                     ==
//==                       Code By WangZhenjiang                     ==
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
public partial class manage_news_Frame : Foosun.Web.UI.ManagePage
{
    ContentManage rd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Special"] == null)
            {
                this.Authority_Code = "C013";
                this.CheckAdminAuthority();
                DataTable TB = rd.sel_JS();
                if (TB != null)
                {
                    this.DropDownList1.DataSource = TB;
                    this.DropDownList1.DataTextField = "JSName";
                    this.DropDownList1.DataValueField = "JsID";
                    this.DropDownList1.DataBind();
                    this.dspecial.Visible = false;
                }
            }
            else
            {
                Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
                DataTable dc = agc.getClassList("SpecialID,SpecialCName,ParentID", "news_special", " Where isLock=0 And isRecyle=0 And SiteID='" + SiteID + "' ");
                listShow(dc, "0", 0, Special);
                dc.Clear(); dc.Dispose();
                this.js.Visible = false;
            }
        }
    }

    protected void listShow(DataTable tempdt, string PID, int Layer, HtmlSelect list)
    {
        DataRow[] row = null;
        row = tempdt.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                string strText = "┝ ";
                for (int j = 0; j < Layer; j++)
                {
                    strText += " ┉ ";
                }
                ListItem itm = new ListItem();
                itm.Value = r[0].ToString();
                itm.Text = strText + r[1].ToString();
                list.Items.Add(itm);
                if (r[0].ToString() != "0")
                    listShow(tempdt, r[0].ToString(), Layer + 1, list);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sid = "";
        if (Request.QueryString["NewsID"] != null)
        {
            sid = Request.QueryString["NewsID"].ToString();
        }
        string GSpecial = Request.QueryString["Special"];
        string[] ID = sid.Split(',');
        string Njf_title = "";
        string PicPath = "";
        string ClassId = "";
        string NewsID = "";
        string Special=Request.Form["Special"];
        string JsID = DropDownList1.SelectedValue;
        if (GSpecial != null)
        {
            if (!string.IsNullOrEmpty(Special))
            {
                string[] SpecialARR = Special.Split(',');
                for (int j = 0; j < ID.Length; j++)
                {
                    for (int m = 0; m < SpecialARR.Length;m++ )
                    {
                        rd.addSpecialTo(ID[j], SpecialARR[m]);
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('选择专题');loaction.href='history.back();';</script>");
            }
        }
        else
        {
            DateTime CreatTime = DateTime.Now;
            DateTime TojsTime = DateTime.Now;
           
            for (int i = 0; i < ID.Length; i++)
            {
                DataTable dt = rd.sel_JSNews(ID[i]);
                if (dt != null)
                {
                    NewsID = dt.Rows[0]["NewsID"].ToString();
                    Njf_title = dt.Rows[0]["NewsTitle"].ToString();
                    PicPath = dt.Rows[0]["PicURL"].ToString();
                    ClassId = dt.Rows[0]["ClassID"].ToString();
                    CreatTime = DateTime.Parse(dt.Rows[0]["CreatTime"].ToString());
                }

                if (rd.Add_JSFile(JsID, Njf_title, NewsID, PicPath, ClassId, SiteID, CreatTime, TojsTime) == 0)
                    break;
            }
            NewsJS nj = new NewsJS();
            NewsJSInfo info = nj.GetSingle(JsID);
            nj.Update(info);
        }
       
        Response.Write("<script language='javascript'>window.opener='anyone';window.close();</script>"); 
    }
}