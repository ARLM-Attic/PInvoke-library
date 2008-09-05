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

public partial class manage_Sys_navimenuEdit : Foosun.Web.UI.ManagePage
{
    public manage_Sys_navimenuEdit()
    {
        Authority_Code = "Q026";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            int id = int.Parse(Request.QueryString["id"]);
            DataTable dt = rd.GetNaviEditID(id);
            menuName.Text = dt.Rows[0]["am_Name"].ToString();
            if (dt.Rows[0]["am_type"].ToString() == "1")
            {
                type.Items.Add("前台");
                type.Items.FindByText("前台").Value = "1";
                type.Items.Add("后台");
                type.Items.FindByText("后台").Value = "0";
            }
            else 
            {
                type.Items.Add("后台");
                type.Items.FindByText("后台").Value = "0";
                type.Items.Add("前台");
                type.Items.FindByText("前台").Value = "1";
            }                          
            type.DataBind();
            if(dt.Rows[0]["isSys"].ToString()=="1")
            {
                isSys.Checked=true;
                FilePath.Text = dt.Rows[0]["am_FilePath"].ToString();
                FilePath.ReadOnly = true;
                f_target.Text = dt.Rows[0]["am_target"].ToString();
                f_target.ReadOnly = true;
                position.Value = dt.Rows[0]["Am_position"].ToString();
                string isablue = "Disabled";
                position.Disabled = true;
                FilePath.Enabled = false;
                f_target.Enabled = false;
                type.Enabled = false;
                isSys.Enabled = false;
                parentIDs.InnerHtml = parentidlist(dt.Rows[0]["am_ParentID"].ToString(), isablue);
            }
            else
            {
                isSys.Checked=false;
                FilePath.Text = dt.Rows[0]["am_FilePath"].ToString();
                f_target.Text = dt.Rows[0]["am_target"].ToString();
                position.Value = dt.Rows[0]["Am_position"].ToString();
                string isablue = "";
                parentIDs.InnerHtml = parentidlist(dt.Rows[0]["am_ParentID"].ToString(), isablue);
            }
            orderID.Text = dt.Rows[0]["am_orderID"].ToString();
            am_id.Value = Request.QueryString["id"];
            Hiddenissys.Value = dt.Rows[0]["isSys"].ToString();
            popCode.Text = dt.Rows[0]["popCode"].ToString();
        }
    }
    
    /// <summary>
    /// 获得主列表
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="isablue"></param>
    /// <returns></returns>
    protected string parentidlist(string parentid, string isablue)
    {
        DataTable dt = rd.Getparentidlist();
        string liststr = "\r<select name=\"parentID\" onChange=\"javascrpt:changevalue(this.value);\" " + isablue + ">\r";
        liststr = liststr + "<option value=\"0\">不指定父菜单[顶部]</option>\r";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Am_position = dt.Rows[i]["Am_position"].ToString();
            string am_id = dt.Rows[i]["am_id"].ToString();
            string am_Name = dt.Rows[i]["am_Name"].ToString();
            string am_ParentID = dt.Rows[i]["am_ParentID"].ToString();
            string am_ClassID = dt.Rows[i]["am_ClassID"].ToString();
            if (parentid == am_ClassID)
            {
                liststr = liststr + "<option value=\"" + am_ClassID + "\" selected>" + am_Name + "</option>\r";
            }
            else
            {
                liststr = liststr + "<option value=\"" + am_ClassID + "\">" + am_Name + "</option>\r";
            }
            if (am_ClassID != "0")
            {
                liststr = liststr + childparentidlist(am_ClassID, "┝", parentid, isablue);
            }
        }
        liststr = liststr + "</select>\r";
        return liststr;
    }

    /// <summary>
    /// 得到子类
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="nchar"></param>
    /// <param name="sparentid"></param>
    /// <param name="isablue"></param>
    /// <returns></returns>
    protected string childparentidlist(string pID, string nchar, string sparentid, string isablue)
    {
        DataTable dt = rd.Getchildparentidlist(pID);
        string TempStr = nchar + "┉";
        string liststr = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string Am_position = dt.Rows[i]["Am_position"].ToString();
            string am_id = dt.Rows[i]["am_id"].ToString();
            string am_Name = dt.Rows[i]["am_Name"].ToString();
            string am_ParentID = dt.Rows[i]["am_ParentID"].ToString();
            string am_ClassID = dt.Rows[i]["am_ClassID"].ToString();
            if (sparentid == am_ClassID)
            {
                liststr = liststr + "<option value=\"" + am_ClassID + "\" selected>" + TempStr + am_Name + "</option>\r";
            }
            else
            {
                liststr = liststr + "<option value=\"" + am_ClassID + "\">" + TempStr + am_Name + "</option>\r";
            }
            if (am_ClassID != "0")
            {
                liststr = liststr + childparentidlist(am_ClassID, TempStr, sparentid, isablue);
            }
        }
        return liststr;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void naviedit(object sender, EventArgs e)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            string issys = this.Hiddenissys.Value;
            string am_Name = this.menuName.Text;;
            int orderID = int.Parse(this.orderID.Text);
            int am_id = int.Parse(this.am_id.Value);
            string popCode = this.popCode.Text;
            if (issys.ToString() == "1")
            {
                Foosun.Model.UserInfo7 uc = new Foosun.Model.UserInfo7();
                uc.am_Name = am_Name;
                uc.am_orderID = orderID;
                uc.am_ID = am_id;
                uc.popCode = popCode;
                rd.EditManageMenu1(uc);
            }
            else
            {
                string Am_position = Request.Form["position"];
                string am_FilePath = Request.Form["FilePath"];
                string am_target = Request.Form["f_target"];
                string am_ParentID = Request.Form["parentID"];
                int am_type = int.Parse(Request.Form["type"]);
                Foosun.Model.UserInfo7 uc = new Foosun.Model.UserInfo7();
                uc.Am_position = Am_position;
                uc.am_Name = am_Name;
                uc.am_FilePath = am_FilePath;
                uc.am_target = am_target;
                uc.am_ParentID = am_ParentID;
                uc.am_type = am_type;
                uc.popCode = popCode;
                uc.am_orderID = orderID;
                if(this.isSys.Checked)
                {
                    uc.isSys = 1;
                }
                else
                {
                    uc.isSys = 0;
                }
                uc.am_ID = am_id;
                rd.EditManageMenu(uc);
            }
            PageRight("修改菜单成功。", "navimenu_list.aspx");
        }
    }
}
