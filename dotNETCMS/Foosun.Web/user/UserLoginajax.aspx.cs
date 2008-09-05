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
using System.Text.RegularExpressions;

public partial class user_UserLoginajax : Foosun.Web.UI.BasePage
{
    protected const string newLine = "\r\n";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        UserOp();
    }

    /// <summary>
    /// 取得操作类型
    /// </summary>
    protected void UserOp()
    {
        string str_Type = Request.QueryString["Type"];
        switch (str_Type)
        {
            case "getLoginForm":
                try
                {
                    CheckUserLogin();
                }
                catch
                {
                    Response.Clear();
                    UserLoginForm();
                }
                Response.Write(UserLoginTrue());
                Response.End();
                break;
            case "Login":
                UserLogin();
                break;
            case "LoginOut":
                UserLoginOut();
                break;
        }
    }

    /// <summary>
    /// 检测用户登录
    /// </summary>
    protected void UserLogin()
    {
        string str_UserNum = Request.QueryString["UserNum"];
        string str_UserPwd = Request.QueryString["UserPwd"];
        try
        {
            Login(str_UserNum, str_UserPwd);
        }
        catch (Exception e)
        {
            Response.Write("ERR$$$" + "用户名或密码不正确");
            Response.End();
        }
        Response.Write("SUC$$$" + UserLoginTrue());
        Response.End();
    }

    /// <summary>
    /// 登录成功之后返回成功列表
    /// </summary>
    /// <returns></returns>
    protected string UserLoginTrue()
    {
        string str_StyleID = Request.QueryString["StyleID"];
        string str_LoginP = Request.QueryString["LoginP"];
        Foosun.CMS.Common.rootPublic rp = new Foosun.CMS.Common.rootPublic();
        string styleContent = rp.getSingleLableStyle(str_StyleID); //取得样式内容

        string str_UserLogin = "";

        if (styleContent != null && styleContent!="")
        {
            try
            {
                string User_Name = "";//用户名
                string guanli = "";//管理中心
                string ziliao = "";//显示资料
                string taolunzu = "";//讨论组连接
                //string tougaoshu="";//投稿数目
                string logout = "";//退出地址
                string guanliAdr = "";//管理中心地址
                string ziliaoAdr = "";//显示资料地址
                string taolunzuAdr = "";//讨论组连接地址
                //string tougaoshu="";//投稿数目
                string logoutAdr = "";//退出地址

                User_Name = Foosun.Global.Current.UserName;
                guanli = "<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx\" target=\"_blank\">会员中心</a>";
                guanliAdr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx";
                if (str_LoginP != "true")
                {
                    guanli = "<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx\" target=\"_blank\">管理</a>";
                    guanliAdr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx";
                }
                ziliao = "<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/userinfo.aspx\" target=\"_blank\">资料</a>";
                ziliaoAdr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/userinfo.aspx";
                taolunzu = "<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/discuss/discussManageestablish_list.aspx\" target=\"_blank\">讨论组</a>";
                taolunzuAdr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/discuss/discussManageestablish_list.aspx";
                logout = "<a href=\"javascript:LoginOut();\">退出</a>";
                logoutAdr = "javascript:LoginOut();";
                string[] arr_UserLogin = Regex.Split(styleContent, @"\$\*\$");
                str_UserLogin = arr_UserLogin[1].ToString();
                //str_UserLogin = styleContent;
                str_UserLogin = str_UserLogin.Replace("{#User_Name}", User_Name);
                str_UserLogin = str_UserLogin.Replace("{#User_AdminCenter}", guanli);
                str_UserLogin = str_UserLogin.Replace("{#User_HomePage}", ziliao);
                str_UserLogin = str_UserLogin.Replace("{#User_DiscussGroup}", taolunzu);
                str_UserLogin = str_UserLogin.Replace("{#User_logout}", logout);

                str_UserLogin = str_UserLogin.Replace("{#User_AdminCenterAdr}", guanliAdr);
                str_UserLogin = str_UserLogin.Replace("{#User_HomePageAdr}", ziliaoAdr);
                str_UserLogin = str_UserLogin.Replace("{#User_DiscussGroupAdr}", taolunzuAdr);
                str_UserLogin = str_UserLogin.Replace("{#User_logoutAdr}", logoutAdr);

                return str_UserLogin;
            }
            catch
            {
            }

        }
        if (str_LoginP == "true")
        {
            str_UserLogin += "   <div style=\"text-align:center;\">" + newLine;
            str_UserLogin += "       欢迎 <span style=\"font-weight:bold;\">" + Foosun.Global.Current.UserName + "</span> 再次登录！";
            str_UserLogin += "       <a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx\" target=\"_blank\">会员中心</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/userinfo.aspx\" target=\"_blank\">资料</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" target=\"_blank\">消息</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/discuss/discussManageestablish_list.aspx\" target=\"_blank\">讨论组</a>";
            str_UserLogin += "       &nbsp;<a href=\"javascript:LoginOut();\">退出</a> ";
            str_UserLogin += "   </div>" + newLine;
        }
        else
        {
            str_UserLogin += "   <div style=\"text-align:center;\">" + newLine;
            str_UserLogin += "       欢迎 <span style=\"font-weight:bold;\">" + Foosun.Global.Current.UserName + "</span> 再次登录！";
            str_UserLogin += "   </div>" + newLine;
            str_UserLogin += "   <div style=\"text-align:center;\">" + newLine;
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Index.aspx\" target=\"_blank\">管理</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/userinfo.aspx\" target=\"_blank\">资料</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/message/Message_box.aspx?Id=1\" target=\"_blank\">消息</a>";
            str_UserLogin += "       &nbsp;<a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/discuss/discussManageestablish_list.aspx\" target=\"_blank\">讨论组</a>";
            str_UserLogin += "       &nbsp;<a href=\"javascript:LoginOut();\">退出</a>";
            str_UserLogin += "   </div>" + newLine;
        }
        return str_UserLogin;
    }

    /// <summary>
    /// 退出
    /// </summary>
    protected void UserLoginOut()
    {
        Logout();
        UserLoginForm();
    }

    /// <summary>
    /// 取得用户登录框
    /// </summary>
    protected void UserLoginForm()
    {
        string str_RandNum = Request.QueryString["RandNum"];
        string str_LoginP = Request.QueryString["LoginP"];
        string str_FormCSS = Request.QueryString["FormCSS"];
        string str_LoginCSS = Request.QueryString["LoginCSS"];
        string str_RegCSS = Request.QueryString["RegCSS"];
        string str_PassCSS = Request.QueryString["PassCSS"];
        string str_StyleID = Request.QueryString["StyleID"];
        string formCSS = "";
        string formlogin = "";
        string resetlogin = "";
        string imgsrc = "";
        string str_UserLogin = "";
        Foosun.CMS.Common.rootPublic rp=new Foosun.CMS.Common.rootPublic();
        string styleContent = "";
        if (str_StyleID != null && str_StyleID != "")
        {
            styleContent=rp.getSingleLableStyle(str_StyleID);
        }
        string Regstr = "注册";
        string RegstrCss = "";
        if (str_RegCSS != string.Empty && str_RegCSS != null)
        {
            RegstrCss = Foosun.Common.Input.URLDecode(str_RegCSS);
        }
        string Passstr = "忘记密码";
        string PassstrCss = "";
        if (str_PassCSS != string.Empty && str_PassCSS != null)
        {
            PassstrCss = Foosun.Common.Input.URLDecode(str_PassCSS);
        }

        if (styleContent != "")
        {
            try
            {
                string NameStr = "";//登陆框
                string PassStr = "";//密码框
                string SimButStr = "";//提交框
                string RegLinkStr = "";//注册连接
                string GetPassStr = "";//找回密码$
                string RegLinkAdrStr = "";//注册连接地址
                string GetPassAdrStr = "";//找回密码地址$
                string ResetStr = "";//重置

                NameStr = "<input name=\"UserNum\" " + formCSS + " type=\"text\" size=\"10\" maxlength=\"20\" />";
                PassStr = "<input name=\"UserPwd\" " + formCSS + " type=\"password\" id=\"UserPwd\" size=\"10\" />";
                SimButStr = "<input style=\"font-size:12px;\" class=\"" + str_LoginCSS + "\" type=\"button\" name=\"but_LoginSubmit\" value=\"登录\" onclick=\"javascript:LoginSubmit(this.form);\" />";
                RegLinkStr = "<span class=\""+RegstrCss+"\"><a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Register.aspx\" title=\"注册新用户\">" + Regstr + "</a></span>";
                GetPassStr = "<span class=\"" + PassstrCss + "\"><a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/getPassword.aspx\" title=\"找回密码\">" + Passstr + "</a></span>"; ;
                RegLinkAdrStr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Register.aspx";
                GetPassAdrStr = Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/getPassword.aspx";
                ResetStr = "<input type=\"reset\" name=\"but_Reset\" class=\"" + str_LoginCSS + "\" value=\"清空\" />";
                string[] arr_UserLogin = Regex.Split(styleContent, @"\$\*\$");
                str_UserLogin = arr_UserLogin[0].ToString();

                str_UserLogin = str_UserLogin.Replace("{#Login_Name}", NameStr);
                str_UserLogin = str_UserLogin.Replace("{#Login_Password}", PassStr);
                str_UserLogin = str_UserLogin.Replace("{#Login_Submit}", SimButStr);
                str_UserLogin = str_UserLogin.Replace("{#Login_Reset}", ResetStr);
                str_UserLogin = str_UserLogin.Replace("{#Reg_LinkUrl}", RegLinkStr);
                str_UserLogin = str_UserLogin.Replace("{#Get_PassLink}", GetPassStr);
                str_UserLogin = str_UserLogin.Replace("{#Reg_LinkUrlAdr}", RegLinkAdrStr);
                str_UserLogin = str_UserLogin.Replace("{#Get_PassLinkAdr}", GetPassAdrStr);
                str_UserLogin = "<form id=\"Form_UserLogin" + str_RandNum + "\" name=\"Form_UserLogin" + str_RandNum + "\" " +
                             "method=\"post\" action=\"\">" + newLine + str_UserLogin;
                str_UserLogin += "</form>" + newLine;
                Response.Write(str_UserLogin);
                Response.End();
            }
            catch
            {
                //错误处理
            }

        }
        else
        {
            if (str_FormCSS != string.Empty && str_FormCSS != null)
            {
                formCSS = " class=" + str_FormCSS + "";
            }
            if (str_LoginCSS != string.Empty && str_LoginCSS != null)
            {
                if (str_LoginCSS.IndexOf(".") > -1)
                {
                    if (str_LoginCSS.IndexOf("http://") > -1)
                    {
                        imgsrc = str_LoginCSS;
                    }
                    else
                    {
                        imgsrc = Foosun.Publish.CommonData.getUrl() + "/" + str_LoginCSS;
                    }
                    formlogin = "<input style=\"font-size:12px;\" type=\"image\" src=\"" + imgsrc + "\" name=\"but_LoginSubmit\" onclick=\"javascript:LoginSubmit(this.form);return false;\" />";
                    resetlogin = "<input type=\"reset\" style=\"font-size:12px;\" name=\"Loginreset\" value=\"重置\" />";
                }
                else
                {
                    formlogin = "<input style=\"font-size:12px;\" class=\"" + str_LoginCSS + "\" type=\"button\" name=\"but_LoginSubmit\" value=\"登录\" onclick=\"javascript:LoginSubmit(this.form);\" />";
                    resetlogin = "<input type=\"reset\" class=\"" + str_LoginCSS + "\" style=\"font-size:12px;\" name=\"Loginreset\" value=\"重置\" />";
                }
            }
            else
            {
                formlogin = "<input style=\"font-size:12px;\" type=\"button\" name=\"but_LoginSubmit\" value=\"登录\" onclick=\"javascript:LoginSubmit(this.form);\" />";
                resetlogin = "<input type=\"reset\" style=\"font-size:12px;\" name=\"Loginreset\" value=\"重置\" />";
            }


            str_UserLogin += "<form id=\"Form_UserLogin" + str_RandNum + "\" name=\"Form_UserLogin" + str_RandNum + "\" " +
                             "method=\"post\" action=\"\">" + newLine;
            if (str_LoginP == "true")
            {
                str_UserLogin += "       帐号：<input name=\"UserNum\" " + formCSS + " type=\"text\" size=\"10\" maxlength=\"20\" />";
                str_UserLogin += "       密码：<input name=\"UserPwd\" " + formCSS + " type=\"password\" id=\"UserPwd\" size=\"10\" />";
                str_UserLogin += "       " + formlogin + "";
                str_UserLogin += "       " + resetlogin + "";
                str_UserLogin += "       <a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/getPassword.aspx\">" + Passstr + "</a>&nbsp;&nbsp;";
                str_UserLogin += "       <a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Register.aspx\">" + Regstr + "</a>";
            }
            else
            {
                str_UserLogin += "   <div>" + newLine;
                str_UserLogin += "       帐号：<input name=\"UserNum\" " + formCSS + " type=\"text\" size=\"10\" maxlength=\"20\" /> " + formlogin + " <span class=\"" + RegstrCss + "\"><a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/info/getPassword.aspx\">" + Passstr + "</a></span>";
                str_UserLogin += "   </div>" + newLine;
                str_UserLogin += "   <div>" + newLine;
                str_UserLogin += "       密码：<input name=\"UserPwd\" " + formCSS + " type=\"password\" id=\"UserPwd\" size=\"10\" /> " + resetlogin + " <span class=\"" + PassstrCss + "\"><a href=\"" + Foosun.Publish.CommonData.getUrl() + "/" + Foosun.Config.UIConfig.dirUser + "/Register.aspx\">" + Regstr + "</a></span>";
                str_UserLogin += "   </div>" + newLine;
            }
            str_UserLogin += "</form>" + newLine;

            Response.Write(str_UserLogin);
            Response.End();
        }
    }
}
