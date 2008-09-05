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
using Foosun.Common;
using Foosun.CMS.Common;

public partial class manage_user_announce_add : Foosun.Web.UI.ManagePage
{
    public manage_user_announce_add()
    {
        Authority_Code = "U020";
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
        }

        GroupList.InnerHtml = groupliststr();
    }

    string groupliststr()
    {
        string _str = "<select Name=\"GroupNumber\">";
        _str += "<option value=\"\">设置会员组浏览权限</option>";
        IDataReader dr = pd.GetGroupList();
        while (dr.Read())
        {
            _str += "<option value=\"" + dr["GroupNumber"].ToString() + "\">" + dr["GroupName"].ToString() + "</option>";
        }
        dr.Close();
        _str += "</select>";
        return _str;
    }

    /// <summary>
    /// sumbitsave 的摘要说明
    /// 数据提交入数据库
    /// </summary>
    protected void sumbitsave(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string TxtTitle = this.title.Text;
            string TxtContent = this.content.Text;
            string TxtGroupNumber = Request.Form["GroupNumber"];
            string TxtgetPoint = this.getPoint.Text;
            DateTime DateCreatTime = System.DateTime.Now;
            if (TxtgetPoint.IndexOf("|") == -1)
            {
                PageError("点数/条件 格式为：1|1|0 格式<br />", "announce.aspx");
            }
            if (TxtTitle.ToString() != "" && TxtTitle.ToString() != null)
            {
                string ramAID;
                ramAID = Rand.Number(12);//产生12位随机字符
                Foosun.Model.UserInfo5 uc = new Foosun.Model.UserInfo5();
                uc.newsID = ramAID;
                uc.Title = TxtTitle;
                uc.content = TxtContent;
                uc.creatTime = DateCreatTime;
                uc.GroupNumber = TxtGroupNumber;
                uc.getPoint = TxtgetPoint;
                uc.SiteId = Foosun.Global.Current.SiteID;
                rd.InsertAnnounce(uc);
                PageRight("创建公告成功成功。", "announce.aspx");
            }
            else
            {
                PageError("请填写公告标题", "");
            }
        }
    }
}
