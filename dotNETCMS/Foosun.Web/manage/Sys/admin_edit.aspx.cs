///************************************************************************************************************
///**********修改管理员信息,Code By DengXi*********************************************************************
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

public partial class manage_Sys_admin_edit : Foosun.Web.UI.ManagePage
{
    public manage_Sys_admin_edit()
    {
        Authority_Code = "Q012";
    }
    public string str_is_Channel = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.CacheControl = "no-cache";                 //设置页面无缓存
            
            copyright.InnerHtml = CopyRight;     //获取版权信息

            string Type = Request.QueryString["Type"];
            if (Type == "Update")
            {
                string ID = Foosun.Common.Input.checkID(Request.QueryString["ID"]);
                ShowAdminInfo(ID);                              //取得管理员信息
            }
        }
    }

    /// <summary>
    /// 从数据库读取管理员信息
    /// </summary>
    /// <param name="ID">管理员ID</param>
    /// <returns>读取管理员信息</returns>
    ///  Code By DengXi
    protected void ShowAdminInfo(string ID)
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        DataTable dt = ac.GetAdminInfo(ID);
        if (dt != null)
        {
            int Cnt = dt.Rows.Count;
            if (Cnt <= 0)
            {
                PageError("参数错误", "");
            }
            //--------------------------向前台输出隐藏域(当前管理员编号,是否超管)-------
            UserNumber.InnerHtml = "<input type=\"hidden\" name=\"UserNum\" value=\"" + ID + "\" /><input type=\"hidden\" name=\"isSuper\" value=\"" + dt.Rows[0]["isSuper"].ToString() + "\" /><input type=\"hidden\" name=\"HisChannel\" value=\"" + dt.Rows[0]["isChannel"].ToString() + "\" />";
            //--------------------------向前台输出隐藏域(当前管理员编号)结束---
            TxtUserName.Text = dt.Rows[0]["UserName"].ToString();
            RealName.Text = dt.Rows[0]["RealName"].ToString();
            Email.Text = dt.Rows[0]["Email"].ToString();
            Iplimited.Text = dt.Rows[0]["Iplimited"].ToString();
            //--------------------------向前台输出管理员组列表-----------------
            GetAdminGroupID(dt.Rows[0]["adminGroupNumber"].ToString());
            //--------------------------向前台输出管理员组列表结束-------------
            //--------------------------取得当前所属站点-----------------------
            SiteList(dt.Rows[0]["SiteID"].ToString());
            //--------------------------取得当前所属站点结束-------------------
            //--------------------------向前台输出单选按钮---------------------

            IsInvocation.Text = dt.Rows[0]["isLock"].ToString();
            isChannel.Text = dt.Rows[0]["isChannel"].ToString();
            str_is_Channel = dt.Rows[0]["isChannel"].ToString();
            isChSupper.Text = dt.Rows[0]["isChSupper"].ToString();
           // MoreLogin.Text = dt.Rows[0]["OnlyLogin"].ToString();
            
            //--------------------------向前台输出单选按钮结束----------------
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            PageError("参数错误", "");
        }
    }


    /// <summary>
    /// 获得管理员组下拉选择框
    /// </summary>
    /// <param name="GroupNum">当前管理员组ID</param>
    /// <returns>返回管理员组下拉选择框</returns>
    ///  Code By DengXi

    protected void GetAdminGroupID(string GroupNum)         
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        DataTable Ds = ac.GetAdminGroupList();
        string Str_GroupTempstr;
        Str_GroupTempstr = "<select name=\"AdminGroup\" style=\"width:206px;\">";
        if (Ds != null)
        {
            int Cnt = Ds.Rows.Count;
            for (int i = 0; i < Cnt; i++)
            {
                string Str_Selected = "";
                if (GroupNum == Ds.Rows[i]["adminGroupNumber"].ToString())
                {
                    Str_Selected = "selected";
                }
                Str_GroupTempstr = Str_GroupTempstr + "<option value=\"" + Ds.Rows[i]["adminGroupNumber"].ToString() + "\" " + Str_Selected + ">" + Ds.Rows[i]["GroupName"].ToString() + "</option>";
            }
            Ds.Clear();
            Ds.Dispose();
        }
        Str_GroupTempstr = Str_GroupTempstr + "</select>";
        Group.InnerHtml = Str_GroupTempstr;        
    }

    /// <summary>
    /// 获取频道列表
    /// </summary>
    /// <param name="SiteID">当前站点ID</param>
    /// <returns>获取频道列表</returns>
    ///  Code By DengXi

    protected void SiteList(string site)
    {
        Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
        DataTable Ds = ac.GetSiteList();

        string Str_SiteIDTempstr;
        Str_SiteIDTempstr = "<select name=\"SiteID\" style=\"width:206px;\">";
        if (Ds != null)
        {
            int Cnt = Ds.Rows.Count;
            Str_SiteIDTempstr = Str_SiteIDTempstr + "<option value=\"0\">请选择频道</option>";
            for (int i = 0; i < Cnt; i++)
            {
                string Str_selected = "";
                if (site == Ds.Rows[i]["ChannelID"].ToString())
                {
                    Str_selected = "selected";
                }
                Str_SiteIDTempstr = Str_SiteIDTempstr + "<option value=\"" + Ds.Rows[i]["ChannelID"].ToString() + " \" " + Str_selected + ">" + Ds.Rows[i]["CName"].ToString() + "</option>";
            }
            Ds.Clear();
            Ds.Dispose();
        }
        Str_SiteIDTempstr = Str_SiteIDTempstr + "</select>";
        Site_Span.InnerHtml = Str_SiteIDTempstr;
    }

    /// <summary>
    /// 更新管理员信息
    /// </summary>
    /// <returns>更新管理员信息</returns>
    ///  Code By DengXi
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            Foosun.Model.AdminInfo aci = new Foosun.Model.AdminInfo();

            //aci.UserNum = Foosun.Common.Input.checkID(Request.Form["TxtUserName"]);
            aci.UserNum = Foosun.Common.Input.checkID(Request.QueryString["ID"]);
            aci.RealName = Request.Form["RealName"];
            aci.Email = Request.Form["Email"];
            aci.SiteID = Request.Form["SiteID"];
            aci.UserPassword = Request.Form["UserPwd"];

            if (aci.UserPassword != null && aci.UserPassword != "" && aci.UserPassword != string.Empty)
                aci.UserPassword = Foosun.Common.Input.MD5(aci.UserPassword, true);

            aci.adminGroupNumber = Request.Form["AdminGroup"];
            aci.OnlyLogin = 1;//int.Parse(MoreLogin.SelectedValue.ToString());
            aci.isChannel = int.Parse(isChannel.SelectedValue.ToString());
            aci.isLock = int.Parse(IsInvocation.SelectedValue.ToString());
            aci.isChSupper = int.Parse(isChSupper.SelectedValue.ToString());
            aci.Iplimited = Request.Form["Iplimited"];

            aci.RegTime = DateTime.Now;
            aci.UserName = "";
            aci.UserGroupNumber = "";

            if (aci.isChannel == 0)
            {
                aci.isChSupper = 0;
            }
            int result = 0;
            Foosun.CMS.Admin ac = new Foosun.CMS.Admin();
            result = ac.Edit(aci);

            if(result==1)
                PageRight("更新管理员信息成功!", "");
            else
                PageError("更新管理员信息失败!", "");
        }
    }
}
