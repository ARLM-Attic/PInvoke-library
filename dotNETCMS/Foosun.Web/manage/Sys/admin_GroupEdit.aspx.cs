///************************************************************************************************************
///**********修改管理员组信息,Code By DengXi*******************************************************************
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


public partial class manage_Sys_admin_GroupEdit : Foosun.Web.UI.ManagePage
{
    public manage_Sys_admin_GroupEdit()
    {
        Authority_Code = "Q017";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = Foosun.Common.Input.checkID(Request.QueryString["ID"]);
        string Type = Request.QueryString["Type"];
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            getList();
            GetGroupInfo(ID);
        }
        if (Type == "Edit")
            UpdateGroupInfo(ID);
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

    protected void listShow(DataTable tempdt, string PID, int Layer, ListBox list)
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
                if (r[0].ToString() != "0")
                    listShow(tempdt, r[0].ToString(), Layer + 1, list);
            }
        }
    }

    /// <summary>
    /// 获取右边列表框
    /// </summary>
    /// <param name="selectname">下拉列表框名字</param>
    /// <param name="list">编号字符串</param>
    /// <returns>获取右边列表框</returns>
    /// Code By DengXi

    protected void GetSelectList(ListBox listbox,string list,string tbname)
    {
        string[] listarr = list.Split(',');
        for (int i = 0; i < listarr.Length; i++)
        {
            string Cname = getName(listarr[i].ToString(), tbname);
            if (Cname != null)
            {
                ListItem itm = new ListItem();
                itm.Value = listarr[i].ToString();
                itm.Text = Cname;
                listbox.Items.Add(itm);
            }
        }
    }


    /// <summary>
    /// 取得组名称以及隐藏域
    /// </summary>
    /// <param name="name">组名称</param>
    /// <param name="newslist">新闻编号字符串</param>
    /// <param name="sitelist">站点编号字符串</param>
    /// <param name="splist">专题编号字符串</param>
    /// <returns>取得组名称以及隐藏域</returns>
    /// Code By DengXi

    protected void GetGroupName(string name,string newslist,string sitelist,string splist)
    {
        Group_Name.InnerHtml = "<input type=\"text\" maxlength=\"20\" id=\"GroupName\" style=\"width:200px;\" value=\"" + name + "\" readonly />";
        Hidden.InnerHtml = "<input id=\"News_List\" name=\"News_List\" type=\"hidden\" value=\"" + newslist + "\" /><input id=\"Site_List\" Name=\"Site_List\" type=\"hidden\" value=\"" + sitelist + "\" /><input id=\"Sp_List\" name=\"Sp_List\" type=\"hidden\" value=\"" + splist + "\" />";
    }

    /// <summary>
    /// 获取管理员组信息
    /// </summary>
    /// <param name="ID">组编号</param>
    /// <returns>获取管理员组信息</returns>
    /// Code By DengXi

    protected void GetGroupInfo(string ID)
    {
        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        DataTable dt = agc.getInfo(ID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                string str_GroupName = dt.Rows[0]["GroupName"].ToString();
                string str_ClassList =dt.Rows[0]["ClassList"].ToString();
                string str_channelList = dt.Rows[0]["channelList"].ToString();
                string str_SpecialList = dt.Rows[0]["SpecialList"].ToString();

                GetGroupName(str_GroupName, str_ClassList, str_channelList, str_SpecialList);

                GetSelectList(NewsClassList2, dt.Rows[0]["ClassList"].ToString(), "news_Class");
                GetSelectList(Site2, dt.Rows[0]["channelList"].ToString(), "news_site");
                GetSelectList(Special2, dt.Rows[0]["SpecialList"].ToString(), "news_special");
            }
            else
            {
                PageError("参数传递错误", "");
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            PageError("参数传递错误", "");
        }
    }


    /// <summary>
    /// 取得专题,栏目,或者站点对应中文名
    /// </summary>
    /// <param name="ID">专题,栏目,或者站点编号</param>
    /// <param name="Tbname">要查询的表名</param>
    /// <returns>返回对应中文名</returns>
    /// Code By DengXi

    protected string getName(string ID, string Tbname)
    {
        string Cname = "";
        string ClassID = "";
        switch (Tbname)
        {
            case "news_Class":
                Cname = "ClassCName";
                ClassID = "ClassID";
                break;
            case "news_site":
                Cname = "CName";
                ClassID = "ChannelID";
                break;
            case "news_special":
                Cname = "SpecialCName";
                ClassID = "SpecialID";
                break;
        }
        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        DataTable dt = agc.getColCname(Cname, Tbname, ClassID, ID);
        string temp_str = null;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                temp_str = dt.Rows[0][Cname].ToString();
            dt.Clear();
            dt.Dispose();
        }
        return temp_str;
    }

    /// <summary>
    /// 修改管理组信息
    /// </summary>
    /// <param name="ID">组编号</param>
    /// <returns>修改管理组信息</returns>
    /// Code By DengXi

    protected void UpdateGroupInfo(string ID)
    {
        int result = 0;
        Foosun.Model.AdminGroupInfo agci = new Foosun.Model.AdminGroupInfo();
        agci.adminGroupNumber = ID;
        agci.GroupName = "";
        agci.ClassList = Foosun.Common.Input.Filter(Request.Form["News_List"]);
        agci.SpecialList = Foosun.Common.Input.Filter(Request.Form["Sp_List"]);
        agci.channelList = Foosun.Common.Input.Filter(Request.Form["Site_List"]);
        agci.CreatTime = DateTime.Now;
        agci.SiteID = SiteID;

        Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
        result = agc.Edit(agci);

        if(result==1)
            PageRight("修改管理员组信息成功!", "admin_group.aspx");
        else
            PageError("修改管理员组信息失败!", "");        
    }
}
