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
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public partial class manage_Sys_File_OnLine_Edit : Foosun.Web.UI.ManagePage
{
    public manage_Sys_File_OnLine_Edit()
    {
        Authority_Code = "Q020";
    }
    sys rd = new sys();
    rootPublic pd = new rootPublic();
    string FP = Foosun.Config.UIConfig.filePass;//从Web.config中读取文件密码信息
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";//设置页面无缓存
        string dir = Request.QueryString["dir"];
        string filename = Request.QueryString["filename"];
        string filepath = dir.Replace("\\\\", "\\") + "\\" + filename;
        string Str_help = "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击查看帮助\" onClick=\"Help('H_FileOnlineEdit_0001',this)\">帮助</span>";//帮助提示
        if (!IsPostBack)//判断页面是否重载
        {
            //判断用户是否登录
            dirPath.InnerHtml = "当前路径:" + dir.Replace("\\\\", "\\") + "&nbsp;&nbsp;&nbsp;&nbsp;当前文件:" + filename + Str_help;
            copyright.InnerHtml = CopyRight; //获取版权信息
            ShowFileContet(filepath);
        }
    }

    /// <summary>
    /// 显示文件内容
    /// </summary>
    ///Code by ChenZhaoHui

    protected void ShowFileContet(string filepath)
    {
        if (File.Exists(filepath))
        {
            try
            {
                StreamReader fso = new StreamReader(filepath);//实例化文件流
                ContentTextBox.Value = fso.ReadToEnd(); //将文件内容读出赋值给文本框
                fso.Close(); //关闭对象
                fso.Dispose();//释放资源
            }
            catch //获取异常
            {
                PageError("对文件访问被拒绝，操作失败！\n请确保您的文件不是只读且权限足够！", "File_GetIn.aspx?id=" + FP + "");
            }
        }
        else
        {
            ContentTextBox.Value = "";
            PageError("参数错误", "File_GetIn.aspx?id=" + FP + "");
        }

    }

    /// <summary>
    /// 保存文件
    /// </summary>
    ///Code by ChenZhaoHui

    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        string dir = Request.QueryString["dir"];
        string filename = Request.QueryString["filename"];
        string filepath = dir.Replace("\\\\", "\\") + "\\" + filename;
        if (File.Exists(filepath))
        {
            try
            {
                StreamWriter fso = new StreamWriter(filepath);//实例化文件流
                fso.Write(ContentTextBox.Value, true); //将文件内容写入到文件
                fso.Close();//关闭对象
                fso.Dispose();//释放资源                    
            }
            catch //获取异常
            {
                PageError("对文件访问被拒绝，操作失败！\n请确保您的文件不是只读且权限足够！", "File_GetIn.aspx?id=" + FP + "");
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "在线编辑文件", "保存成功.");
            PageRight("保存成功", "File_GetIn.aspx?id=" + FP + "");
        }
        else
        {
            PageError("参数错误", "File_GetIn.aspx?id=" + FP + "");
        }
    }
}
