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

public partial class manage_Sys_Navimenu_list : Foosun.Web.UI.ManagePage
{
    public manage_Sys_Navimenu_list()
    {
        Authority_Code = "Q025";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        navimenu_list.InnerHtml = navimenusub();
        string action = Request.QueryString["action"];
        if (action == "del")
        {
            this.Authority_Code = "Q026";
            this.CheckAdminAuthority();
            int qID = int.Parse(Request.QueryString["ID"]);
            string qClassID = Request.QueryString["ClassID"];
            Shortcutdel(qID, qClassID);
        }
    }

    protected void Shortcutdel(int qID, string ClassID)
    {
        if (ClassID.Trim() == "")
        {
            PageError("错误的参数(参数传递错误)。", "");
        }
        else
        {
            rd.Shortcutdel(qID);
            rd.Shortcutde2(ClassID);
            PageRight("删除成功。", "navimenu_list.aspx");
        }
    }
    string navimenusub()//显示快捷菜单
    {
        //显示列表开始
        string type = Request.QueryString["type"];
        string sqlStr = "";
        switch (type)
        {
            case "manage":
                sqlStr = " and am_type=0";
                break;
            case "user":
                sqlStr = " and am_type=1";
                break;
            case "top":
                sqlStr = " and am_position='00000'";
                break;
            case "normal":
                sqlStr = " and am_position<>'00000' and am_position<>'999999'";
                break;
            case "api":
                sqlStr = " and am_position='99999'";
                break;
            case "sys":
                sqlStr = " and issys=1";
                break;
            case "unsys":
                sqlStr = " and isSys=0";
                break;
            case "all":
                sqlStr = "";
                break;
            default:
                sqlStr = " and am_position='00000'";
                break;
        }
        string liststr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
        liststr = liststr + "<tr class=\"TR_BG\">\r";
        liststr = liststr + "<td width='100'class=\"sys_topBg\">名称</td>\r";
        liststr = liststr + "<td width='60'class=\"sys_topBg\">位置</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">连接地址</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">窗口</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">类型</td>\r";
        liststr = liststr + "<td  width=\"60\" align='left'class=\"sys_topBg\">系统菜单</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">频道</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">操作</td>\r";
        liststr = liststr + "</tr>";
        DataTable dt = rd.navimenusub(sqlStr);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string am_Name = dt.Rows[i]["am_Name"].ToString();
            string Am_position = dt.Rows[i]["Am_position"].ToString();
            string positionstr = "顶部";
            if (Am_position == "00000")
            {
                positionstr = "顶部";
            }
            else
            {
                if (Am_position == "99999")
                {
                    positionstr = "API菜单";
                }
                else
                {
                    positionstr = "普通菜单";
                }
            }
            string am_FilePath = dt.Rows[i]["am_FilePath"].ToString();
            string am_ClassID = dt.Rows[i]["am_ClassID"].ToString();
            string am_target = dt.Rows[i]["am_target"].ToString();
            string am_type = dt.Rows[i]["am_type"].ToString();
            string strTye ="后台";
            if (am_type == "0")
            {
                strTye = "后台";
            }
            else
            {
                strTye = "前台";
            }
            string isSys = dt.Rows[i]["isSys"].ToString();
            string strisSys = "后台";
            if (isSys == "0")
            {
                strisSys = "用户";
            }
            else
            {
                strisSys = "系统";
            }
            string siteID = dt.Rows[i]["siteID"].ToString();
            if (siteID == "0")
            {
                siteID = "主站";
            }
            string id = dt.Rows[i]["am_id"].ToString();
            
            liststr = liststr + "<tr class=\"TR_BG_list\">\r";
            liststr = liststr + "<td width='100'class=\"list_link\">" + am_Name + "</td>\r";
            liststr = liststr + "<td width='60'class=\"list_link\">" + positionstr + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\"><div style=\"font-size:11px\">" + am_FilePath + "</div></td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + am_target + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + strTye + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + strisSys + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + siteID + "</td>\r";
            if (isSys == "0")
            {
                liststr = liststr + "<td align='left'class=\"list_link\"><a class=\"list_link\" href=\"navimenuEdit.aspx?id=" + id + "\">修改</a>&nbsp;┊&nbsp;<a class=\"list_link\" href=\"Navimenu_list.aspx?action=del&id=" + id + "&ClassID=" + am_ClassID + "\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\">删除</a></td>\r";
            }
            else
            {
                liststr = liststr + "<td align='left'class=\"list_link\"><a class=\"list_link\" href=\"navimenuEdit.aspx?id=" + id + "\">修改</a>&nbsp;┊&nbsp;<font color=\"999999\">删除</font><span class=\"helpstyle\" onClick=\"Help('H_navimenulist_0001',this)\">帮助</span></td>\r";
            }
                liststr = liststr + "</tr>";
        }
        liststr = liststr + "</table>";
        return liststr;
        //显示列表结束
    }
}
