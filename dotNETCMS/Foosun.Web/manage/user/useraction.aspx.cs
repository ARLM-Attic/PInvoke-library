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

public partial class manage_user_useraction : Foosun.Web.UI.ManagePage
{

    UserList UL = new UserList();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            hidden_uid.Value =Request.QueryString["uid"];
            string PointType = Request.QueryString["PointType"];
            actionContent.InnerHtml = ContentAction(PointType);
            string sPointType = Request.QueryString["sPointType"];
            string uid = Request.QueryString["uid"];
            string Point = Request.QueryString["point"];
            switch (sPointType)
            {
                case "bIpoint":
                    bIpoint(uid, Point);
                    break;
                case "sIpoint":
                    sIpoint(uid, Point);
                    break;
                case "bGpoint":
                    bGpoint(uid, Point);
                    break;
                case "sGpoint":
                    sGpoint(uid, Point);
                    break;
                default:
                    break;
            }
        }        
    }

    string ContentAction(string PointType)
    {
        string titleStr = "";
        string actionStr = "";
        switch (PointType)
        { 
            case "bIpoint":
                this.Authority_Code = "U005";
                this.CheckAdminAuthority();
                titleStr = "增加积分";
                actionStr = "bIpoint";
                break;
            case "sIpoint":
                this.Authority_Code = "U006";
                this.CheckAdminAuthority();
                titleStr = "扣除积分";
                actionStr = "sIpoint";
                break;
            case "bGpoint":
                this.Authority_Code = "U007";
                this.CheckAdminAuthority();
                titleStr = "增加G币";
                actionStr = "bGpoint";
                break;
            case "sGpoint":
                this.Authority_Code="U008";
                this.CheckAdminAuthority();
                titleStr = "扣除G币";
                actionStr = "sGpoint";
                break;
            default:
                break;          
        }
        string liststr = "";
        liststr += titleStr;
        liststr += "&nbsp;<input type=\"text\" name=\"Point\" value=\"0\">";
        liststr += "&nbsp;<input type=\"button\" onclick=\"" + actionStr + "(document.form1.hidden_uid.value,document.form1.Point.value);\" value=\"执行此操作\">";
        return liststr;
    }

    protected void bIpoint(string uid, string sPoint)
    {
        int intPoint = 0;
        try
        {
            intPoint = int.Parse(sPoint);
        }
        catch(Exception bi)
        {
            PageError(bi.ToString(), "");
        }

        if (UL.bIpoint(Foosun.Common.Input.Filter(uid), intPoint) == 0)
        {
            PageError("增加积分失败", "UserList.aspx");
        }
        else
        {
            //此处插入日志记录
            PageRight("增加积分成功", "UserList.aspx");
        }
    }

    protected void sIpoint(string uid, string sPoint)
    {
        int intPoint = 0;
        try
        {
            intPoint = int.Parse(sPoint);
        }
        catch (Exception bi)
        {
            PageError(bi.ToString(), "");
        }

        if (UL.sIpoint(uid, intPoint) == 0)
        {
            PageError("减少积分失败", "UserList.aspx");
        }
        else
        {
            //此处插入日志记录

            PageRight("减少积分成功", "UserList.aspx");
        }
    }

    protected void bGpoint(string uid, string sPoint)
    {
        int intPoint = 0;
        try
        {
            intPoint = int.Parse(sPoint);
        }
        catch (Exception bi)
        {
            PageError(bi.ToString(), "");
        }

        if (UL.bGpoint(uid, intPoint) == 0)
        {
            PageError("增加G币失败", "UserList.aspx");
        }
        else
        {
            //此处插入日志记录

            PageRight("增加G币成功", "UserList.aspx");
        }
    }

    protected void sGpoint(string uid, string sPoint)
    {
        int intPoint = 0;
        try
        {
            intPoint = int.Parse(sPoint);
        }
        catch (Exception bi)
        {
            PageError(bi.ToString(), "");
        }

        if (UL.sGpoint(uid, intPoint) == 0)
        {
            PageError("减少G币失败", "UserList.aspx");
        }
        else
        {
            //此处插入日志记录

            PageRight("减少G币成功", "UserList.aspx");
        }
    }
}
