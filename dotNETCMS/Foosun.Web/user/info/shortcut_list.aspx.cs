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

public partial class user_info_shortcut_list : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            copyright.InnerHtml = CopyRight;
        }
        shortcut_list.InnerHtml = ShortcutList();
        string action = Request.QueryString["action"];
        if (action == "del")
        {
            int qID = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["ID"].ToString()));
            Shortcutdel(qID);
        }
    }
    protected void Shortcutdel(int qID)
    {
        rd.QShortcutdel(qID, 0);
        PageRight("删除成功。", "shortcut_list.aspx");
    }

    string ShortcutList()//显示快捷菜单
    {
        IDataReader dr = rd.QShortcutList(0);
        string liststr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
        liststr = liststr + "<tr class=\"TR_BG\">\r";
        liststr = liststr + "<td width='60'class=\"sys_topBg\">序号</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">名称</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">链接地址</td>\r";
        liststr = liststr + "<td align='left'class=\"sys_topBg\">操作</td>\r";
        liststr = liststr + "</tr>";
        while (dr.Read())
        {
            int id = int.Parse(dr["id"].ToString());
            string qName = dr["qName"].ToString();
            string FilePath = dr["FilePath"].ToString();
            string usernum = dr["usernum"].ToString();
            string orderid = dr["orderid"].ToString();

            liststr = liststr + "<tr class=\"TR_BG_list\">\r";
            liststr = liststr + "<td width='60'class=\"list_link\">" + orderid + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + qName + "</td>\r";
            liststr = liststr + "<td align='left'class=\"list_link\">" + FilePath + "</td>\r";
            if (usernum == "0")
            {
                liststr = liststr + "<td align='left'class=\"list_link\"><span style=\"color:#999999\">系统菜单</span></td>\r";
            }
            else
            {
                liststr = liststr + "<td align='left'class=\"list_link\"><a class=\"list_link\" href=\"shortcut.aspx?action=edit&id=" + id + "\"><img src=\"../../sysImages/folder/re.gif\" border=\"0\"></a>&nbsp;<a class=\"list_link\" href=\"shortcut_list.aspx?action=del&id=" + id + "\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/folder/dels.gif\" border=\"0\"></a></td>\r";
            }
            liststr = liststr + "</tr>";
        }
        dr.Close();
        liststr = liststr + "</table>";
        return liststr;
        //显示列表结束
    }
}
