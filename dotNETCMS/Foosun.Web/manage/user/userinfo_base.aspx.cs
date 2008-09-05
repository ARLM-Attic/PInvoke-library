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

public partial class manage_user_userinfo_base : Foosun.Web.UI.ManagePage
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
        catch (Exception ES)
        {
            PageError("传递的参数应该为数字。<li>错误描述：<br />" + ES.ToString() + "</li>", "");
        }
        lockTF.InnerHtml = locks(suid);
        adminTF.InnerHtml = admins(suid);
        GroupList.InnerHtml = GroupLists(suid);
        isCerts.InnerHtml = Certs(suid);
        DataTable udt = rd.getUserInfoBaseStat(suid);
        if (udt != null)
        {
            if (udt.Rows.Count > 0)
            {
                CertType.Text = udt.Rows[0]["CertType"].ToString();
                CertNumber.Text = udt.Rows[0]["CertNumber"].ToString();
                ipoint.Text = udt.Rows[0]["ipoint"].ToString();
                gpoint.Text = udt.Rows[0]["gpoint"].ToString();
                cpoint.Text = udt.Rows[0]["cpoint"].ToString();
                epoint.Text = udt.Rows[0]["epoint"].ToString();
                apoint.Text = udt.Rows[0]["apoint"].ToString();
                RegTime.Text = udt.Rows[0]["RegTime"].ToString();
                onlineTime.Text = udt.Rows[0]["onlineTime"].ToString();
                LoginNumber.Text = udt.Rows[0]["LoginNumber"].ToString();
                LoginLimtNumber.Text = udt.Rows[0]["LoginLimtNumber"].ToString();
                lastIP.Text = udt.Rows[0]["lastIP"].ToString();
                TxtSite.Text = udt.Rows[0]["SiteID"].ToString();
                LastLoginTime.Text = udt.Rows[0]["LastLoginTime"].ToString();
                userID.Value = udt.Rows[0]["id"].ToString();
            }
            udt.Dispose();
        }
    }

    string locks(int suid)
    {
        string liststr = "<select class=\"form\" name=\"islock\" style=\"width:150px\">";
        DataTable dt = rd.getLockStat(suid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["islock"].ToString() == "1")
                {
                    liststr += "<option value=\"1\" selected>锁定</option>";
                }
                else
                {
                    liststr += "<option value=\"1\">锁定</option>";
                }

                if (dt.Rows[0]["islock"].ToString() == "0")
                {
                    liststr += "<option value=\"0\" selected>正常</option>";
                }
                else
                {
                    liststr += "<option value=\"0\">正常</option>";
                }
            }
            dt.Dispose();
        }
        liststr += "</select>";
        return liststr;
    }

    string admins(int suid)
    {
        string liststr = "<select class=\"form\" name=\"isadmin\" style=\"width:150px\">";
        DataTable dt = rd.getAdminsStat(suid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["isadmin"].ToString() == "1")
                {
                    liststr += "<option value=\"1\" selected>是</option>";
                }
                else
                {
                    liststr += "<option value=\"1\">是</option>";
                }

                if (dt.Rows[0]["isadmin"].ToString() == "0")
                {
                    liststr += "<option value=\"0\" selected>否</option>";
                }
                else
                {
                    liststr += "<option value=\"0\">否</option>";
                }
            }
            dt.Dispose();
        }
        liststr += "</select>";
        return liststr;
    }

    string GroupLists(int suid)
    {
        string liststr = "<select class=\"form\" name=\"GroupNumber\">";
        DataTable udt = rd.getGroupListStat(suid);
        if (udt != null)
        {
            if (udt.Rows.Count > 0)
            {
                Foosun.CMS.Common.rootPublic rp = new Foosun.CMS.Common.rootPublic();
                IDataReader dr = rp.GetGroupList();
                while (dr.Read())
                {
                    if (udt.Rows[0]["UserGroupNumber"].ToString() == dr["GroupNumber"].ToString())
                    {
                        liststr += "<option value=\"" + dr["GroupNumber"] + "\" selected>" + dr["GroupName"] + "</option>";
                    }
                    else
                    {
                        liststr += "<option value=\"" + dr["GroupNumber"] + "\">" + dr["GroupName"] + "</option>";
                    }
                }
                dr.Close();
            }
            udt.Dispose();
        }
        return liststr;
    }


    protected string Certs(int suid)
    {
        string liststr = "";
        DataTable dt = rd.getCertsStat(suid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["isIDcard"].ToString() == "1")
                {
                    liststr += "<font color=blue>已通过认证</font>";
                }
                else
                {
                    liststr += "<font color=red>未通过认证</font>";
                }
            }
            dt.Dispose(); 
        }
        return liststr;
    
    }

    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int UserID = 0, lockTF = 0, adminTF = 0, ipoint = 0, gpoint = 0, cpoint = 0, epoint = 0, apoint = 0, onlineTime = 0, LoginNumber = 0, LoginLimtNumber = 0;
            DateTime RegTime = System.DateTime.Now;
            try
            {
                UserID = Convert.ToInt32(Request.Form["userID"]);
                lockTF = Convert.ToInt32(Request.Form["islock"]);
                adminTF = Convert.ToInt32(Request.Form["isadmin"]);
                ipoint = Convert.ToInt32(Request.Form["ipoint"]);
                gpoint = Convert.ToInt32(Request.Form["gpoint"]);
                cpoint = Convert.ToInt32(Request.Form["cpoint"]);
                epoint = Convert.ToInt32(Request.Form["epoint"]);
                apoint = Convert.ToInt32(Request.Form["apoint"]);
                RegTime = DateTime.Parse(Request.Form["RegTime"]);
                onlineTime = Convert.ToInt32(Request.Form["onlineTime"]);
                LoginNumber = Convert.ToInt32(Request.Form["LoginNumber"]);
                LoginLimtNumber = Convert.ToInt32(Request.Form["LoginLimtNumber"]);
            }
            catch (Exception us)
            {
                PageError("错误的参数<br />" + us.ToString() + "", "");
            }

            string LastLoginTime = Request.Form["LastLoginTime"];
            string GroupList = Request.Form["GroupNumber"];
            string CertType = Request.Form["CertType"];
            string CertNumber = Request.Form["CertNumber"];
            string lastIP = Request.Form["lastIP"];
            string ReqSite = Request.Form["TxtSite"];

            Foosun.Model.UserInfo3 uc = new Foosun.Model.UserInfo3();
            uc.Id = UserID;
            uc.UserGroupNumber = GroupList;
            uc.islock = lockTF;
            uc.isadmin = adminTF;
            uc.CertType = CertType;
            uc.CertNumber = CertNumber;
            uc.ipoint = ipoint;
            uc.gpoint = gpoint;
            uc.cpoint = cpoint;
            uc.epoint = epoint;
            uc.apoint = apoint;
            uc.onlineTime = onlineTime;
            uc.RegTime = RegTime;
            if (LastLoginTime != null && LastLoginTime != "")
            {
                uc.LastLoginTime = DateTime.Parse(LastLoginTime);
            }
            else
            {
                uc.LastLoginTime = System.DateTime.Now;
            }
            uc.LoginNumber = LoginNumber;
            uc.LoginLimtNumber = LoginLimtNumber;
            uc.lastIP = lastIP;
            uc.SiteID = ReqSite;
            rd.UpdateUserInfoBaseStat(uc);
            PageRight("修改资料成功。", "userlist.aspx");
        }
    }
}
