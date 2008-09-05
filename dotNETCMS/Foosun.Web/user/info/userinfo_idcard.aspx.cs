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

public partial class user_info_userinfo_idcard : Foosun.Web.UI.UserPage
{
    public user_info_userinfo_idcard()
    {
        UserCertificate = false;
    }
    UserMisc rd = new UserMisc();
    public string f1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            copyright.InnerHtml = CopyRight;
            #region 查询数据
            DataTable dt = rd.getICardTF();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["isIDcard"].ToString() == "1")
                    {
                        f1 = "1";
                        icardcertstat.InnerHtml = "<span class=\"tbie\">已经认证</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"userinfo_idcard.aspx?type=reviewCert\" class=\"list_link\">查看附件</a>";
                    }
                    else
                    {
                        if (dt.Rows[0]["IDcardFiles"].ToString().Trim() != string.Empty)
                        {
                            f1 = "1";
                            icardcertstat.InnerHtml = "未通过认证,正在审核中....&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"userinfo_idcard.aspx?type=reviewCert\" class=\"list_link\">查看附件</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"userinfo_idcard.aspx?isReset=resetCert\" class=\"list_link\" OnClick=\"{if(confirm('确定要取消认证吗？')){return true;}return false;}\">取消认证</a>";
                        }
                        else
                        {
                            f1 = "1";
                            icardcertstat.InnerHtml = "未认证&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"userinfo_idcard.aspx?type=CreatCert\" class=\"tbie\">认证实名</a>";
                        }
                    }
                }
                string typestr = Request.QueryString["type"];
                if (typestr != "" && typestr != null)
                {
                    f1 = "";
                    if (typestr.ToString() == "reviewCert")
                    {
                        f1 = "1";
                        isCertstat.InnerHtml = "<table width=\"98%\" border=\"0\" cellpadding=\"8\" cellspacing=\"0\" class=\"table\" align=\"center\"><tr><td align=\"center\"><img src=\"" + dt.Rows[0]["IDcardFiles"].ToString().ToLower().Replace("{@userdirfile}", Foosun.Config.UIConfig.UserdirFile) + "\" border=\"0\" /></td></tr></table>";
                    }
                    else
                    {
                        isCertstat.InnerHtml = "<table width=\"60%\" border=\"0\" cellpadding=\"8\" cellspacing=\"0\" class=\"table\" align=\"center\"><tr><td align=\"center\" class=\"TR_BG_list\">上传图片(证件扫描件)&nbsp;<input style=\"width:50%\" type=\"text\" name=\"f_IDcardFiles\" / class=\"form\"><img src=\"../../sysImages/folder/s.gif\" style=\"cursor:pointer;\" alt=\"选择图片\" border=\"0\"  onclick=\"selectFile('user_pic',document.form1.f_IDcardFiles,300,500);document.form1.f_IDcardFiles.focus();\" /><br /><br /><img src=\"../../sysImages/normal/nopic.gif\" border=\"0\" id=\"imgsrc\" /><br /><br /><input name=\"buton\" value=\"开始认证\" onclick=\"subup();\" type=\"button\" class=\"form\"/></td></tr></table>";
                    }
                }
            }
            #endregion 查询数据结束
        }
        #region 取消认证
        string isReset = Request.QueryString["isReset"];
        if (isReset != null && isReset != "")
        {
            rd.ResetICard();
            PageRight("取消成功", "userinfo_idcard.aspx");
        }
        #endregion 取消人证
        #region 判断是否是上传
        string isCert = Request.QueryString["isCert"];
        if (isCert != null && isCert != "")
        {
            savedata();
        }
        #endregion 判断上传结束
    }

    /// <summary>
    /// 保存上传认证数据
    /// </summary>
    protected void savedata()
    {
        string f_IDcardFiles = Foosun.Common.Input.Htmls(Request.Form["f_IDcardFiles"]);
        if (f_IDcardFiles == null || f_IDcardFiles == string.Empty)
        {
            PageError("正确填写图片地址", "javascript:history.back();");
        }
        if (f_IDcardFiles.IndexOf("/") == -1 || f_IDcardFiles.Length<5)
        {
            PageError("正确填写图片地址", "javascript:history.back();");
        }
        rd.SaveDataICard(f_IDcardFiles);
        PageRight("上传认证图片认证成功，等待审核", "userinfo_idcard.aspx");
    }
}
