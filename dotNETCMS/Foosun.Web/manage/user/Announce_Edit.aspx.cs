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

public partial class manage_user_AnnounceEdit : Foosun.Web.UI.ManagePage
{
    public manage_user_AnnounceEdit()
    {
        Authority_Code = "U022";
    }
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            #region 为表单赋值
            if (Request.QueryString["id"] != null || Request.QueryString["id"] != "")
            {
                int aId=0;
                try
                {
                    aId = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["id"]));
                    DataTable dt = rd.getAnnounceEdit(aId);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            this.title.Text = dt.Rows[0]["title"].ToString();
                            this.content.Text = dt.Rows[0]["content"].ToString();
                            this.getPoint.Text = dt.Rows[0]["getpoint"].ToString();
                            GroupList.InnerHtml = groupliststr(dt.Rows[0]["GroupNumber"].ToString());
                            this.aId.Value = dt.Rows[0]["id"].ToString();
                        }
                    }
                }
                catch(Exception AX)
                {
                    PageError("错误的参数。<li>" + AX + "</li>", "");
                }
            }
            #endregion 为表单赋值
        }
    }

    string groupliststr(string GroupNumber)
    {
        string _str = "<select Name=\"GroupNumber\">";
        _str += "<option value=\"\">设置会员组浏览权限</option>";
        IDataReader dr = pd.GetGroupList();
        while (dr.Read())
        {
            if (GroupNumber.ToString() == dr["GroupNumber"].ToString())
            {
                _str += "<option value=\"" + dr["GroupNumber"].ToString() + "\" selected>" + dr["GroupName"].ToString() + "</option>";
            }
            else
            {
                _str += "<option value=\"" + dr["GroupNumber"].ToString() + "\">" + dr["GroupName"].ToString() + "</option>";
            }
        }
        dr.Close();
        _str += "</select>";
        return _str;
    }
    /// <summary>
    /// 更新数据
    /// 编码时间2007年2月27日
    /// </summary>
    protected void sumbitsave(object sender, EventArgs e)
    {
        #region 更新数据
        if (Page.IsValid)
        {
            string TxtTitle = this.title.Text;
            string TxtContent = this.content.Text;
            string TxtGroupNumber = Request.Form["GroupNumber"];
            string TxtgetPoint = this.getPoint.Text;
            int aId=0;
            aId = int.Parse(this.aId.Value);
            if (TxtgetPoint.IndexOf("|") == -1)
            {
                PageError("点数/条件 格式为：1|1|0 格式<br />", "announce.aspx");
            }
            if (TxtTitle.ToString() != "" && TxtTitle.ToString() != null)
            {
                Foosun.Model.UserInfo5 uc = new Foosun.Model.UserInfo5();
                uc.Id = aId;
                uc.Title = TxtTitle;
                uc.content = TxtContent;
                uc.GroupNumber = TxtGroupNumber;
                uc.getPoint = TxtgetPoint;
                rd.UpdateAnnounce(uc); 
                PageRight("修改公告 [" + TxtTitle + "] 成功。", "announce.aspx");
            }
            else
            {
                PageError("请填写公告标题", "");
            }
        }
        #endregion 更新数据

    }
}