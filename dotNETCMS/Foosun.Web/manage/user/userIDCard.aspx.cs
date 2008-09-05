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

public partial class manage_user_userIDCard : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        string uid = Foosun.Common.Input.Filter(Request.QueryString["id"]);
        int suid = 0;
        try
        {
            suid = int.Parse(uid);
        }
        catch (Exception es)
        {
            PageError("传递的参数应该为数字。" + es.ToString(), "");
        }
        userStat.InnerHtml = userStatlist(suid);
        idCard.InnerHtml = idCardlist(suid);
        string actionLock = Request.QueryString["Action"];
        if (actionLock != null && actionLock != "")
        {
            Lockstat(actionLock,suid);
        }
    }

    protected void Lockstat(string actionLock, int suid)
    {
        string str1 = "";
        string str2 = "";
        if (actionLock == "unLock")
        {
            str1 = " set isIDcard=1";
            str2 = "审核";
        }
        else
        {
          str1 = " set isIDcard=0";
          str2 = "取消审核";
        }
        rd.UpdateUserInfoIDCard(suid,str1);
        PageRight("" + str2 + "认证成功。", "userlist.aspx");
    }

    protected string userStatlist(int suid)
    {
        string liststr = "";
        string userNames = "";
        string isCert = "";
        DataTable dt = rd.userStatlist(suid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                 userNames = dt.Rows[0]["UserName"].ToString();
                 if (dt.Rows[0]["isIDcard"].ToString() == "1")
                 { 
                   isCert = "<font color=blue>已认证审核</font>" ;
                 }
                 else
                 {
                   isCert = "<font color=red>未认证审核</font>" ;
                 }
            }
            else
            {
                PageError("找不到此用户的记录。", "");
            }
        }
        else
        {
            PageError("找不到此用户的记录。", "");
        }

        liststr += "您目前正在审核<font color=red>" + userNames + "</font>的认证附件...目前<font color=red>" + userNames + "</font>用户的附件审核状态："+isCert+"";
        return liststr;
    }


    string idCardlist(int suid)
    {
        string idfiles = "";
        string isCerts = "";
        string liststr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" class=\"table\">";
        liststr+= "<tr class=\"TR_BG_list\">";
        DataTable dt = rd.idCardlist(suid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                idfiles = dt.Rows[0]["IDcardFiles"].ToString().Replace("{@userdirfile}", Foosun.Config.UIConfig.UserdirFile);
                if (dt.Rows[0]["isIDcard"].ToString() == "1")
                {
                    isCerts = "&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"list_link\" href=\"userIDCard.aspx?Action=Lock&Id=" + dt.Rows[0]["id"] + "\" title=\"点击取消认证\" onclick=\"{if(confirm('确认取消认证吗？')){return true;}return false;}\">取消认证</a>";
                }
                else
                {
                    isCerts = "&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"list_link\" href=\"userIDCard.aspx?Action=unLock&Id=" + dt.Rows[0]["id"] + "\" onclick=\"{if(confirm('确认认证通过吗？')){return true;}return false;}\">认证</a>";
                }
            }
            else
            {
                idfiles = "";
            }
        }
        string filsPath = "";
        string filshref = "";
        if (Foosun.Config.UIConfig.dirDumm.Trim() != null && Foosun.Config.UIConfig.dirDumm.Trim() != "")
        {
            if (idfiles == "" || idfiles == string.Empty || idfiles == null)
            {
                filsPath = "/" + Foosun.Config.UIConfig.dirDumm + "/sysImages/folder/nofiles.gif";
                filshref += "无附件";
            }
            else
            {
                filsPath = "/" + Foosun.Config.UIConfig.dirDumm + idfiles;
                filshref += "<a class=\"list_link\" href=\"" + filsPath + "\" target=\"_blank\">查看附件</a>" + isCerts + "";
            }
        }
        else
        {
            if (idfiles == "" || idfiles == string.Empty || idfiles == null)
            {
                filsPath = "/sysImages/folder/nofiles.gif";
                filshref += "无附件";
            }
            else
            {
                filsPath =  idfiles;
                filshref += "<a class=\"list_link\" href=\"" + filsPath + "\" target=\"_blank\">查看附件</a>" + isCerts + "";
            }
        }
        liststr+= "<td width=\"240px\" align=\"center\">";
        liststr+= "<img src=\"" + filsPath + "\"  border=\"0\" width=\"230\" />";
        liststr+= "</td>";
        liststr += "<td valign=\"top\">";
        liststr += "<br />" + filshref + "";
        liststr += "</td>";
        liststr += "</table>";
        return liststr;
    }
}
