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

public partial class user_show_showphoto : Foosun.Web.UI.BasePage
{
    Photo pho = new Photo();
    Discuss dis = new Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PhotoalbumID = Request.QueryString["PhotoalbumID"];
            string uid = Request.QueryString["uid"];
            if (PhotoalbumID == "" && PhotoalbumID == null)
            {
                PageError("错误参数", "");
            }

            DataTable dt_phopwd = pho.sel(Foosun.Common.Input.Filter(PhotoalbumID.ToString()));
            if (dt_phopwd.Rows[0][0].ToString() != "" && Foosun.Global.Current.UserNum != dt_phopwd.Rows[0][1].ToString())
            {
                this.Panel1.Visible = true;
                this.Panel2.Visible = false;
            }
            else
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = true;
            }
            Show_cjlist(1);
            if (this.Panel2.Visible == true)
            {
                sc.InnerHtml = Show_scs(PhotoalbumID.ToString(), uid.ToString());
            }
        }
    }

    protected void PageNavigator2_PageChange(object sender, int PageIndex2)
    {
        Show_cjlist(PageIndex2);
    }
    protected void Show_cjlist(int PageIndex2)
    {
        string PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        int ib, jb;
        SQLConditionInfo st = new SQLConditionInfo("@PhotoalbumID", PhotoalbumID);
        DataTable cjlistdts = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex2, 10, out ib, out jb, st);

        this.PageNavigator2.PageCount = jb;
        this.PageNavigator2.PageIndex = PageIndex2;
        this.PageNavigator2.RecordCount = ib;

        if (cjlistdts.Rows.Count > 0)
        {
            cjlistdts.Columns.Add("UserNamess", typeof(string));
            cjlistdts.Columns.Add("PhotoalbumName", typeof(string));
            cjlistdts.Columns.Add("PhotoUrls", typeof(string));
            string _dirDumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirDumm.Trim() != ""){_dirDumm = _dirDumm + "/";}
            foreach (DataRow r in cjlistdts.Rows)
            {
                r["UserNamess"] = dis.sel_60(r["UserNum"].ToString());
                r["PhotoalbumName"] = dis.sel_61(r["PhotoalbumID"].ToString());
                r["PhotoUrls"] = r["PhotoUrl"].ToString().Replace("{@userdirfile}", _dirDumm + Foosun.Config.UIConfig.UserdirFile);
            }
            DataList1.DataSource = cjlistdts;
            DataList1.DataBind();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator2.Visible = false;
        }
    }

    protected void open_Click(object sender, EventArgs e)
    {
        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"]);
        string pwd = Foosun.Common.Input.Filter(Request.Form["pwd"]);
        DataTable dt = pho.sel(PhotoalbumIDs);
        if (dt.Rows[0][0].ToString() != Foosun.Common.Input.MD5(pwd, true))
        {
            PageError("密码错误", "Photoalbumlist.aspx");
        }
        else
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
        }
    }


    string Show_no()
    {
        string nos = "<table border=0 width='98%' cellpadding=\"5\" cellspacing=\"1\" class=\"table\" align=\"center\">";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    } 

    protected string Show_scs(string PhotoalbumIDs,string uID)
    {
        string scs = "<a href=\"showphotofilt.aspx?PhotoalbumID=" + PhotoalbumIDs + "&uid=" + uID + "\" class=\"menulist\">幻灯播放</a>";
        return scs;
    }
}
