///************************************************************************************************************
///**********添加管理员组,Code By DengXi***********************************************************************
///************************************************************************************************************
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

public partial class manage_Sys_admin_GroupAdd : Foosun.Web.UI.ManagePage
{
    public manage_Sys_admin_GroupAdd()
    {
        Authority_Code = "Q017";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            getList();
        }
        string Str_Type = Request.QueryString["Type"];
        if (Str_Type == "Add")
            GroupAdd();
    }

    /// <summary>
    /// 取得频道，栏目，专题的DataTable
    /// </summary>
    /// <returns></returns>

    protected void getList()
    {
        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        DataTable dt = agc.getClassList("ClassID,ClassCName,ParentID", "news_Class", " Where isLock=0 And isRecyle=0 And SiteID='" + SiteID + "' ");
        DataTable dv = agc.getClassList("ChannelID,CName,ParentID", "news_site", " Where isLock=0 And isRecyle=0 And SiteID='" + SiteID + "' ");
        DataTable dc = agc.getClassList("SpecialID,SpecialCName,ParentID", "news_special", " Where isLock=0 And isRecyle=0 And SiteID='" + SiteID + "' ");
        listShow(dt, "0", 0, NewsClassList);
        listShow(dv, "0", 0, Site1);
        listShow(dc, "0", 0, Special1);
    }

    /// <summary>
    /// 在ListBox中呈现出来
    /// </summary>
    /// <param name="tempdt">DataTable</param>
    /// <param name="PID">父类编号</param>
    /// <param name="Layer">层次</param>
    /// <param name="list">ListBox控件名称</param>
    
    protected void listShow(DataTable tempdt, string PID, int Layer,ListBox list)
    {
        DataRow[] row = null;
        row = tempdt.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                string strText = "┝";
                for (int j = 0; j < Layer; j++)
                {
                    strText += "┉";
                }
                ListItem itm = new ListItem();
                itm.Value = r[0].ToString();
                itm.Text = strText + r[1].ToString();
                list.Items.Add(itm);
                if (r[0].ToString()!="0")
                    listShow(tempdt, r[0].ToString(), Layer + 1, list);
            }
        }    
    }

    /// <summary>
    /// 添加管理员组信息
    /// </summary>
    /// <returns>添加管理员组信息</returns>
    /// Code By DengXi

    protected void GroupAdd()
    {
        //querystring 方式改为 form方式提交数据,避免数据量过大引起错误 arjun
        int result = 0;
        Foosun.Model.AdminGroupInfo agci = new Foosun.Model.AdminGroupInfo();
        agci.adminGroupNumber = "";
        agci.GroupName = Foosun.Common.Input.Filter(Request.Form["GroupName"]);
        agci.ClassList = Foosun.Common.Input.Filter(Request.Form["News_List"]);
        agci.channelList = Foosun.Common.Input.Filter(Request.Form["Site_List"]);
        agci.SpecialList = Foosun.Common.Input.Filter(Request.Form["Sp_List"]);
        agci.SiteID = SiteID;
        agci.CreatTime = DateTime.Now;

        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        result = agc.add(agci);

        if(result ==1)
            PageRight("添加管理员组成功!", "admin_group.aspx");
        else
            PageError("添加管理员组失败!", "");
    }

}
