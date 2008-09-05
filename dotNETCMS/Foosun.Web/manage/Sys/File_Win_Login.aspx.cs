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

public partial class manage_Sys_File_Win_Login : Foosun.Web.UI.ManagePage
{
    public manage_Sys_File_Win_Login()
    {
        Authority_Code = "Q020";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //判断页面是否重载
        {
             //判断用户是否登录
            if (SiteID != "0") { PageError("您没有管理文件的权限", ""); }
            //LoginInfo.CheckPop("权限代码", "0", "1", "9"); //权限代码
            copyright.InnerHtml = CopyRight;  //获取版权信息
            Response.CacheControl = "no-cache"; //清除缓存
        }
    }

    /// <summary>
    /// 根据传递的密码进入文件管理
    /// </summary>
    ///Code by ChenZhaoHui
   
    protected void FilePassClick_ServerClick(object sender, EventArgs e)
    {
        string fp = Foosun.CMS.FSSecurity.FDESEncrypt(File_Manag_Pass.Text.ToString().Trim(), 1);
        fp = fp.Replace("+", "%2B");
        Response.Redirect("File_GetIn.aspx?id="+fp);
    }
}
