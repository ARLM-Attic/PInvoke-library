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

public partial class user_info_UpdateGroup : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string UserGroupNumber = Request.QueryString["UserGroupNumber"];
            if (UserGroupNumber != null && UserGroupNumber != "")
            {
                GroupName.InnerHtml = pd.getGroupName(UserGroupNumber.ToString());
            }
            else
            {
                PageError("错误的编号", "");
            }
            copyright.InnerHtml = CopyRight;
            groupList.InnerHtml = GetGroupList();
        }
    }

    protected string GetGroupList()
    {
        string _Str = "<select name=\"GroupNumber\">";
        IDataReader dr = pd.GetGroupList();
        while (dr.Read())
        {
            _Str += "<option value=\"" + dr["GroupNumber"] + "\">" + dr["GroupName"] + "</option>\r";
        }
        dr.Close();
        _Str += "</select>";
        return _Str;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
