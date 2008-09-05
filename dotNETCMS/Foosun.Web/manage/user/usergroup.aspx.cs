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

public partial class manage_user_usergroup : Foosun.Web.UI.ManagePage
{
    public manage_user_usergroup()
    {
        Authority_Code = "U011";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        string DelStr = Request.QueryString["Action"];
        if (DelStr == "del")
        {
            int gId = 0;
            try
            {
                gId = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["id"]));
            }
            catch (Exception gus)
            {
                PageError("错误的参数" + gus.ToString() + "", "");
            }
            dels(gId);
        }
        grouplist.InnerHtml = GroupStr();
    }
    
    protected void dels(int gid)
    {
        this.Authority_Code = "U014";
        this.CheckAdminAuthority();
        rd.GroupDels(gid);
        PageRight("删除会员组成功。", "UserGroup.aspx");
    }

    string GroupStr()
    {
        string liststr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
        liststr += "<tr class=\"TR_BG\" align=\"center\">";
        liststr += "<td class=\"sys_topBg\" style=\"width:80px\">编号</td>";
        liststr += "<td class=\"sys_topBg\" style=\"width:40px\">折扣</td>";
        liststr += "<td class=\"sys_topBg\" style=\"width:150px\">会员组名</td>";
        liststr += "<td class=\"sys_topBg\" style=\"width:70px\">点数</td>";
        liststr += "<td class=\"sys_topBg\" style=\"width:70px\">G币</td>";
        liststr += "<td class=\"sys_topBg\" style=\"width:100px\">创建日期</td>";
        liststr += "<td class=\"sys_topBg\">人数</td>";
        liststr += "<td class=\"sys_topBg\">操作</td>";
        liststr += "</tr>";
        DataTable dt = rd.GroupListStr();
        if(dt!=null)
        {
            for (int i = 0; dt.Rows.Count > i; i++)
            {
                liststr += "<tr class=\"TR_BG_list\" onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">";
                string _Discount = "";
                if (dt.Rows[i]["Discount"] != null && dt.Rows[i]["Discount"].ToString() != string.Empty)
                {
                    _Discount = dt.Rows[i]["Discount"].ToString();
                }
                else
                {
                    _Discount = "无折扣";
                }
                liststr += "<td align=\"center\">" + dt.Rows[i]["GroupNumber"] + "</td>";
                liststr += "<td align=\"center\">" + _Discount + "</td>";
                liststr += "<td align=\"center\">" + dt.Rows[i]["GroupName"] + "</td>";
                liststr += "<td align=\"center\">" + dt.Rows[i]["iPoint"] + "</td>";
                liststr += "<td align=\"center\">" + dt.Rows[i]["Gpoint"] + "</td>";
                liststr += "<td>" + dt.Rows[i]["CreatTime"] + "</td>";
                DataTable dts = rd.GetGroupRecord(dt.Rows[i]["GroupNumber"].ToString());
                if (dts != null)
                {
                    liststr += "<td align=\"center\">" + dts.Rows.Count + "</td>";
                }
                else
                {
                    liststr += "<td align=\"center\">0</td>";
                }
                liststr += "<td align=\"center\"><a href=\"UserGroupEdit.aspx?id=" + dt.Rows[i]["id"] + "\" class=\"list_link\"><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt='修改'></a><a href=\"UserGroup.aspx?id=" + dt.Rows[i]["id"] + "&Action=del\" onClick=\"{if(confirm('确定要删除吗？')){return true;}return false;}\" class=\"list_link\"><img src=\"../../sysimages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt='删除'></a></td>";
                liststr += "</tr>";
                dts.Clear(); dts.Dispose();
            }
            dt.Clear(); dt.Dispose();
        }
        liststr += "</table>";
        return liststr;

    }
}
